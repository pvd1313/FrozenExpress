using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Abstractions
{
    public abstract class ADebugDrawer
    {
        public abstract void DrawCube(Vector3 position, Vector3 size, Color32 color);
    }
}