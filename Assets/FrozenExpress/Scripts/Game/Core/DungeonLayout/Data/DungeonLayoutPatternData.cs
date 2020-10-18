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
        public EDungeonLayoutPatternFlip Flip => _flip;

        [SerializeField] private Mesh _mesh;
        [SerializeField] private EDungeonLayoutPatternTrigger _trigger;
        [SerializeField] private EDungeonLayoutPatternFlip _flip;
        [SerializeField] private EDungeonLayoutPatternRotation _rotation;
        [SerializeField] private List<DungeonLayoutPatternCellExistsCondition> _cellExistConditions;
    }
}
