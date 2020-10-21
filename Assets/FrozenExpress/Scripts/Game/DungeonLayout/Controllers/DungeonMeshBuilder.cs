using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonMeshBuilder : IDungeonMeshBuilder
    {
        private readonly IDungeonPatternController _patternMaster;
        private readonly IDungeonTransform _layoutTransform;
        private readonly List<CombineInstance> _combineInstances;
        private readonly DungeonMap _dungeonLayoutMap;

        public DungeonMeshBuilder(IDungeonPatternController patternMaster,
                                        IDungeonTransform layoutTransform)
        {
            _patternMaster = patternMaster;
            _layoutTransform = layoutTransform;
            _combineInstances = new List<CombineInstance>();
            _dungeonLayoutMap = new DungeonMap();
        }

        public void BuildMesh(DungeonLayout dungeonLayout, Mesh result)
        {
            _dungeonLayoutMap.Initialize(dungeonLayout);

            _combineInstances.Clear();
            foreach (var entity in _patternMaster.IterateEntities(_dungeonLayoutMap))
            {
                var position = _layoutTransform.TransformPosition(entity.Position);
                _combineInstances.Add(new CombineInstance()
                {
                    mesh = entity.Mesh,
                    transform = Matrix4x4.TRS(position, entity.Rotation, entity.Scale)
                });
            }
            
            result.CombineMeshes(_combineInstances.ToArray());
        }
    }
}
