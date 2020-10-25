namespace FrozenExpress.Scripts.Game.Debugging.Abstractions
{
    public abstract class ADebugFactory
    {
        public abstract ADebugLogger CreateLogger(object owner);

        public abstract ADebugDrawer CreateDrawer(object owner);
    }
}