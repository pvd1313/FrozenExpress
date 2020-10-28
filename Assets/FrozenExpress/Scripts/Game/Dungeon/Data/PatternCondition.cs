using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class PatternCondition
    {
        public string Variable => _variable;
        public Vector3Int Direction => _direction;
        public int Layer => _layer;

        [SerializeField] private string _variable;
        [SerializeField] private Vector3Int _direction;
        [SerializeField] private int _layer;
    }
}
