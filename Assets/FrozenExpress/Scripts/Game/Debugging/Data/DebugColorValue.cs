using FrozenExpress.Scripts.Game.Debugging.Enums;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Data
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