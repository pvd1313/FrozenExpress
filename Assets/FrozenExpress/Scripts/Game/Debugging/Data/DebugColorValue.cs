using UnityEngine;

namespace FrozenExpress.Game.Debugging
{
    [System.Serializable]
    public class DebugColorValue
    {
        public EDebugColor Color => _color;
        public Color Value => _value;

        [SerializeField] private EDebugColor _color;
        [SerializeField] private Color _value;
    }
}