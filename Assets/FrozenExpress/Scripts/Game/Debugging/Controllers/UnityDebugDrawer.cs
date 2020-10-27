using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Debugging.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    public class UnityDebugDrawer : ADebugDrawer
    {
        private readonly object _owner;
        private readonly IDebugDrawingController _debugDrawingController;

        public UnityDebugDrawer(object owner, IDebugDrawingController debugDrawingController)
        {
            _owner = owner;
            _debugDrawingController = debugDrawingController;
        }
        
        public override void Cube(Vector3 position, Vector3 size, Color32 color, float duration)
        {
            _debugDrawingController.DrawCube(position, size, color, duration);
        }
    }
}