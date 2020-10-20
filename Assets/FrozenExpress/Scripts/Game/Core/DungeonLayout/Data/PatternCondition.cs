using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Serializable]
    public class PatternCondition
    {
        public Vector3Int Direction => _direction;
        public bool MustExist => _mustExist;

        [SerializeField] private Vector3Int _direction;
        [SerializeField] private bool _mustExist;
    }
}
