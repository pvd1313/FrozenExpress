using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutMeshBuilder : IDungeonLayoutMeshBuilder
    {
        private readonly IDungeonLayoutPatternMaster _patternMaster;
        private readonly IDungeonLayoutTransform _layoutTransform;
        private readonly List<CombineInstance> _combineInstances;
        private readonly DungeonLayoutMap _dungeonLayoutMap;

        public DungeonLayoutMeshBuilder(IDungeonLayoutPatternMaster patternMaster,
                                        IDungeonLayoutTransform layoutTransform)
        {
            _patternMaster = patternMaster;
            _layoutTransform = layoutTransform;
            _combineInstances = new List<CombineInstance>();
            _dungeonLayoutMap = new DungeonLayoutMap();
        }

        public void BuildMesh(DungeonLayoutData dungeonLayout, Mesh result)
        {
            _dungeonLayoutMap.Initialize(dungeonLayout);

            _combineInstances.Clear();
            foreach (var entity in _patternMaster.IterateEntities(_dungeonLayoutMap))
            {
                var position = _layoutTransform.TransformPosition(entity.Position);
                _combineInstances.Add(new CombineInstance()
                {
                    mesh = entity.Mesh,
                    transform = Matrix4x4.TRS(position, entity.Rotation, Vector3.one)
                });
            }
            
            result.CombineMeshes(_combineInstances.ToArray());
        }
    }
}
