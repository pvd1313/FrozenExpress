using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Serializable]
    public class DungeonLayoutPatternData
    {
        public EDungeonLayoutPatternTrigger Trigger => _trigger;
        public IReadOnlyList<DungeonLayoutPatternCellExistsCondition> CellExistConditions => _cellExistConditions;
        public EDungeonLayoutPatternRotation Rotation => _rotation;
        public Mesh Mesh => _mesh;

        [SerializeField] private EDungeonLayoutPatternTrigger _trigger;
        [SerializeField] private List<DungeonLayoutPatternCellExistsCondition> _cellExistConditions;
        [SerializeField] private EDungeonLayoutPatternRotation _rotation;
        [SerializeField] private Mesh _mesh;
    }
}
