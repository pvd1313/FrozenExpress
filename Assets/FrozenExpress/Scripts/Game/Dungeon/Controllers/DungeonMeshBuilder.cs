using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonMeshBuilder : IDungeonMeshBuilder
    {
        private readonly IDungeonPatternController _patternMaster;
        private readonly IDungeonTransform _layoutTransform;
        private readonly IMeshBuilder _meshBuilder;

        public DungeonMeshBuilder(IDungeonPatternController patternMaster,
                                  IDungeonTransform layoutTransform,
                                  IMeshBuilder meshBuilder)
        {
            _patternMaster = patternMaster;
            _layoutTransform = layoutTransform;
            _meshBuilder = meshBuilder;
        }

        public void BuildMesh(IReadOnlyDictionary<Vector3Int, int> dungeonLayout, Mesh result)
        {
            _meshBuilder.Clear();

            foreach (var entity in _patternMaster.IterateEntities(dungeonLayout))
            {
                var position = _layoutTransform.TransformPosition(entity.Position);
                var transform = Matrix4x4.TRS(position, entity.Rotation, entity.Scale);

                _meshBuilder.AddMesh(entity.Mesh, transform, entity.VertexColor);
            }

            _meshBuilder.Apply(result);
        }
    }
}
