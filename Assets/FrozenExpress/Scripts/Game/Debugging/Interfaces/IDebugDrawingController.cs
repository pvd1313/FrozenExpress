using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Interfaces
{
    public interface IDebugDrawingController
    {
        void DrawCube(Vector3 position, Vector3 size, Color32 color);
    }
}