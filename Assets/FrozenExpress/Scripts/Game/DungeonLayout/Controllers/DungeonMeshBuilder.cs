using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonMeshBuilder : IDungeonMeshBuilder
    {
        private readonly IDungeonPatternController _patternMaster;
        private readonly IDungeonTransform _layoutTransform;
        private readonly IMeshBuilder _meshBuilder;
        private readonly DungeonMap _dungeonLayoutMap;

        public DungeonMeshBuilder(IDungeonPatternController patternMaster,
                                  IDungeonTransform layoutTransform,
                                  IMeshBuilder meshBuilder)
        {
            _patternMaster = patternMaster;
            _layoutTransform = layoutTransform;
            _meshBuilder = meshBuilder;
            _dungeonLayoutMap = new DungeonMap();
        }

        public void BuildMesh(DungeonLayout dungeonLayout, Mesh result)
        {
            _dungeonLayoutMap.Initialize(dungeonLayout);
            _meshBuilder.Clear();
            
            foreach (var entity in _patternMaster.IterateEntities(_dungeonLayoutMap))
            {
                var position = _layoutTransform.TransformPosition(entity.Position);
                var transform = Matrix4x4.TRS(position, entity.Rotation, entity.Scale);
                _meshBuilder.AddMesh(entity.Mesh, transform, Color.white);
            }

            _meshBuilder.Apply(result);
        }
    }
}
