namespace FrozenExpress.Scripts.Game.Logging.Controllers
{
    public abstract class ALogFactory
    {
        public abstract ALogger Create(object owner);
    }
}
