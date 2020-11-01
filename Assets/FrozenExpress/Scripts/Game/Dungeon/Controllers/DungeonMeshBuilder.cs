using System.Collections.Generic;
using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonMeshBuilder : IDungeonMeshBuilder, ITerminatable
    {
        private readonly IDungeonTransform _layoutTransform;
        private readonly IMeshBuilder _meshBuilder;
        private readonly IDungeonPatternTransformController _patternTransformer;
        private readonly IDungeonEntityDatabase _entityDatabase;
        
        private readonly Dictionary<DungeonEntity, Mesh> _meshCash;
        private readonly Vector3 _scaleOne;
        private readonly Quaternion _rotationIdentity;

        public DungeonMeshBuilder(IDungeonTransform layoutTransform,
                                  IMeshBuilder meshBuilder,
                                  IDungeonPatternTransformController patternTransformer,
                                  IDungeonEntityDatabase entityDatabase)
        {
            _layoutTransform = layoutTransform;
            _meshBuilder = meshBuilder;
            _patternTransformer = patternTransformer;
            _entityDatabase = entityDatabase;
            
            _scaleOne = new Vector3(1, 1, 1);
            _rotationIdentity = Quaternion.identity;
            _meshCash = new Dictionary<DungeonEntity, Mesh>();

            _entityDatabase.OnCashUpdate += UpdateMeshCash;
        }

        private void UpdateMeshCash()
        {
            _meshCash.Clear();
            foreach (var entity in _entityDatabase.All)
            {
                var mesh = entity.Mesh;
                if (mesh != null)
                {
                    _meshCash.Add(entity, mesh);
                }
            }
        }

        public void BuildMesh(IEnumerable<EntityPatternTransform> entityTransforms, Mesh result)
        {
            _meshBuilder.Clear();

            foreach (var entityTransform in entityTransforms)
            {
                var entity = entityTransform.Entity;
                if (!_meshCash.TryGetValue(entity, out var entityMesh))
                {
                    return;
                }

                var position = _layoutTransform.TransformPosition(entityTransform.Position);
                
                var scale = _scaleOne;
                var rotation = _rotationIdentity;
                var transform = entityTransform.Transform;
                for (var i = 0; i < transform.Count; i++)
                {
                    var operation = transform[i];
                    scale = _patternTransformer.TransformScale(scale, operation);
                    rotation = _patternTransformer.TransformRotation(rotation, operation);
                }

                var matrix = Matrix4x4.TRS(position, rotation, scale);
                
                _meshBuilder.AddMesh(entityMesh, matrix, entity.DebugVertexColor);
            }
            
            _meshBuilder.Apply(result);
        }

        public void Terminate()
        {
            _entityDatabase.OnCashUpdate -= UpdateMeshCash;
        }
    }
}
