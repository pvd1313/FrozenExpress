using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Debugging.Interfaces;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    public class UnityDebugFactory : ADebugFactory
    {
        private readonly IDebugDrawingController _debugDrawingController;

        public UnityDebugFactory(IDebugDrawingController debugDrawingController)
        {
            _debugDrawingController = debugDrawingController;
        }
        
        public override ADebugLogger CreateLogger(object owner)
        {
            return new UnityDebugLogger(owner);
        }

        public override ADebugDrawer CreateDrawer(object owner)
        {
            return new UnityDebugDrawer(owner, _debugDrawingController);
        }
    }
}
