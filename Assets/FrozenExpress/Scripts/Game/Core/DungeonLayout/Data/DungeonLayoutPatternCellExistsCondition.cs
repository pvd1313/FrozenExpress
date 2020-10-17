using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Serializable]
    public class DungeonLayoutPatternCellExistsCondition
    {
        public Vector3Int Offset => _offset;
        public bool MustExist => _mustExist;

        [SerializeField] private Vector3Int _offset;
        [SerializeField] private bool _mustExist;
    }
}
