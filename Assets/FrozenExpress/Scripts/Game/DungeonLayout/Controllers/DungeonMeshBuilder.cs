using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonMeshBuilder : IDungeonMeshBuilder
    {
        private readonly IDungeonPatternController _patternMaster;
        private readonly IDungeonTransform _layoutTransform;
        private readonly IMeshBuilder _meshBuilder;
        private readonly List<IDungeonMeshVertexColorProvider> _meshColorProviders;
        private readonly DungeonMap _dungeonLayoutMap;

        public DungeonMeshBuilder(IDungeonPatternController patternMaster,
                                  IDungeonTransform layoutTransform,
                                  IMeshBuilder meshBuilder,
                                  List<IDungeonMeshVertexColorProvider> meshColorProviders)
        {
            _patternMaster = patternMaster;
            _layoutTransform = layoutTransform;
            _meshBuilder = meshBuilder;
            _meshColorProviders = meshColorProviders;
            _dungeonLayoutMap = new DungeonMap();
        }

        public void BuildMesh(DungeonLayout dungeonLayout, Mesh result)
        {
            _dungeonLayoutMap.Initialize(dungeonLayout);
            _meshBuilder.Clear();

            var color = new Color32(1, 1, 1, 1);

            foreach (var entity in _patternMaster.IterateEntities(_dungeonLayoutMap))
            {
                var position = _layoutTransform.TransformPosition(entity.Position);
                var transform = Matrix4x4.TRS(position, entity.Rotation, entity.Scale);
                if (_meshColorProviders.Count > 0)
                {
                    color = _meshColorProviders[0].GetColor(entity.Mesh);
                }

                _meshBuilder.AddMesh(entity.Mesh, transform, color);
            }

            _meshBuilder.Apply(result);
        }
    }
}
