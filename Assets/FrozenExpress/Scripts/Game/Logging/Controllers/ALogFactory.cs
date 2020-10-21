namespace FrozenExpress.Game
{
    public abstract class ALogFactory
    {
        public abstract ALogger Create(object owner);
    }
}
