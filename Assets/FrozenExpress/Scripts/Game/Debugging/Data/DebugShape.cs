using UnityEngine;

namespace FrozenExpress.Game.Debugging
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