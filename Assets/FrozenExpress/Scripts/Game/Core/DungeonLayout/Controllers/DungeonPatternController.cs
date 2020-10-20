using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonPatternController : IDungeonPatternController
    {
        private readonly Vector3 _scaleOne = new Vector3(1, 1, 1);
        private readonly Quaternion _rotationIdentity = Quaternion.identity;

        private readonly IReadOnlyList<Pattern> _patterns;
        private readonly IDungeonPatternTransformController _transformer;

        private readonly List<Pattern> _patternBuffer;
        private readonly List<Vector3Int> _cellBuffer;

        public DungeonPatternController(IReadOnlyList<Pattern> patterns,
                                          IDungeonPatternTransformController transformer)
        {
            _patterns = patterns;
            _transformer = transformer;
            _patternBuffer = new List<Pattern>();
            _cellBuffer = new List<Vector3Int>();
        }

        public IEnumerable<DungeonEntity> IterateEntities(IDungeonMap map)
        {
            _patternBuffer.Clear();
            _patternBuffer.AddRange(_patterns.Where(p => p.Trigger == EPatternTrigger.Cell));
            
            _cellBuffer.Clear();
            _cellBuffer.AddRange(map.Cells);

            for (var i = 0; i < _patternBuffer.Count; i++)
            {
                var pattern = _patternBuffer[i];
                var conditions = pattern.Conditions;

                for (var j = 0; j < _cellBuffer.Count; j++)
                {
                    var cell = _cellBuffer[j];
                    
                    for (var k = 0; k < pattern.MatrixSequences.Count; k++)
                    {
                        var matrixSequence = pattern.MatrixSequences[k].Sequence;
                        
                        for (var p = 0; p < conditions.Count; p++)
                        {
                            var condition = conditions[p];
                            var direction = condition.Direction;
                            for (var q = 0; q < matrixSequence.Count; q++)
                            {
                                direction = _transformer.TransformDirection(direction, matrixSequence[q]);
                            }

                            var exists = map.CheckCellExists(cell + direction);
                            if (exists != condition.MustExist)
                            {
                                break;
                            }

                            var scale = _scaleOne;
                            var rotation = _rotationIdentity;
                            for (var q = 0; q < matrixSequence.Count; q++)
                            {
                                scale = _transformer.TransformScale(scale, matrixSequence[q]);
                                rotation = _transformer.TransformRotation(rotation, matrixSequence[q]);
                            }

                            yield return new DungeonEntity()
                            {
                                Mesh = pattern.Mesh,
                                Position = cell,
                                Rotation = rotation,
                                Scale = scale
                            };
                        }
                    }
                }
            }
        }
    }
}
