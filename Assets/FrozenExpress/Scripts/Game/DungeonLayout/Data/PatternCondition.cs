using OneLine;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    [System.Serializable]
    public class PatternCondition
    {
        public string Variable => _variable;
        public Vector3Int Direction => _direction;

        [SerializeField] private string _variable;
        [SerializeField] private Vector3Int _direction;
    }
}
