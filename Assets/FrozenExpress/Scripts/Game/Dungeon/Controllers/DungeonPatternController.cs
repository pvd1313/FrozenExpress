using System.Collections.Generic;
using System.Linq;
using Bunny83.LSystem;
using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonPatternController : IDungeonPatternController, ITerminatable
    {
        private readonly Vector3 _scaleOne = new Vector3(1, 1, 1);
        private readonly Quaternion _rotationIdentity = Quaternion.identity;
        
        private readonly IDungeonPatternTransformController _transformer;
        private readonly IDungeonPatternExpressionParser _expressionParser;
        private readonly IDungeonEntityDatabase _entityDatabase;
        private readonly IDungeonEntityPatternDatabase _entityPatternDatabase;

        private readonly List<EntityPattern> _patternBuffer;
        private readonly List<Vector3Int> _cellBuffer;
        private readonly List<PatternCondition> _conditionBuffer;
        private readonly Dictionary<EntityPattern, LogicExpression> _expressionCash;

        public DungeonPatternController(IDungeonPatternTransformController transformer,
                                        IDungeonPatternExpressionParser expressionParser,
                                        IDungeonEntityDatabase entityDatabase,
                                        IDungeonEntityPatternDatabase entityPatternDatabase)
        {
            _entityDatabase = entityDatabase;
            _entityPatternDatabase = entityPatternDatabase;
            _transformer = transformer;
            _expressionParser = expressionParser;
            _patternBuffer = new List<EntityPattern>();
            _cellBuffer = new List<Vector3Int>();
            _conditionBuffer = new List<PatternCondition>();
            _expressionCash = new Dictionary<EntityPattern, LogicExpression>();

            _entityPatternDatabase.OnCashUpdate += UpdateExpressionCash;
        }

        private void UpdateExpressionCash()
        {
            _expressionCash.Clear();
            foreach (var pattern in _entityPatternDatabase.All)
            {
                var expression = _expressionParser.CompileExpression(pattern);
                if (expression != null)
                {
                    _expressionCash.Add(pattern, expression);
                }
            }
        }

        public IEnumerable<MeshTransform> IterateEntities(IReadOnlyDictionary<Vector3Int, int> layoutMap)
        {
            _patternBuffer.Clear();
            _patternBuffer.AddRange(_entityPatternDatabase.All.Where(p => p.Trigger == EPatternTrigger.Cell));
            
            _cellBuffer.Clear();
            _cellBuffer.AddRange(layoutMap.Keys);

            for (var i = 0; i < _patternBuffer.Count; i++)
            {
                var pattern = _patternBuffer[i];
                _expressionCash.TryGetValue(pattern, out var expression);
                var entity = _entityDatabase.GetEntity(pattern.EntityId);

                if (expression == null || entity == null)
                {
                    continue;
                }

                var variables = expression.Context.variables;

                _conditionBuffer.Clear();
                var conditionsInsideExpression = pattern.Condition.Conditions.Where(c => variables.ContainsKey(c.Variable));
                _conditionBuffer.AddRange(conditionsInsideExpression);

                for (var j = 0; j < _cellBuffer.Count; j++)
                {
                    var cell = _cellBuffer[j];
                    
                    for (var k = 0; k < pattern.MatrixSequences.Count; k++)
                    {
                        var matrixSequence = pattern.MatrixSequences[k].Sequence;
                        
                        for (var p = 0; p < _conditionBuffer.Count; p++)
                        {
                            var condition = _conditionBuffer[p];
                            var direction = condition.Direction;
                            for (var q = 0; q < matrixSequence.Count; q++)
                            {
                                direction = _transformer.TransformDirection(direction, matrixSequence[q]);
                            }

                            layoutMap.TryGetValue(cell + direction, out var directionLayer);
                            var conditionResult = (directionLayer & condition.Layer) == condition.Layer;
                            variables[condition.Variable].Set(conditionResult);
                        }

                        if (!expression.GetResult())
                        {
                            continue;
                        }

                        var scale = _scaleOne;
                        var rotation = _rotationIdentity;
                        for (var q = 0; q < matrixSequence.Count; q++)
                        {
                            scale = _transformer.TransformScale(scale, matrixSequence[q]);
                            rotation = _transformer.TransformRotation(rotation, matrixSequence[q]);
                        }

                        MeshTransform result;
                        result.Mesh = entity.Mesh;
                        result.Position = cell;
                        result.Rotation = rotation;
                        result.Scale = scale;
                        result.VertexColor = entity.DebugVertexColor;

                        yield return result;
                    }
                }
            }
        }

        public void Terminate()
        {
            _entityPatternDatabase.OnCashUpdate += UpdateExpressionCash;
        }
    }
}
