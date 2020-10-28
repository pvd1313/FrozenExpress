using FrozenExpress.Game.Debugging;

namespace FrozenExpress.Game.Debugging
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
