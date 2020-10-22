using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Data
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
