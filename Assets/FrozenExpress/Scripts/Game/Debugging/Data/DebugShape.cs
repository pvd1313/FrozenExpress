using FrozenExpress.Scripts.Game.Debugging.Enums;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Data
{
    public struct DebugShape
    {
        public EDebugShader Shader;
        public EDebugShape Shape;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public float Duration;
        public EDebugColor Color;
    }
}