namespace FrozenExpress.Scripts.Game.Logging.Controllers
{
    public class UnityLogFactory : ALogFactory
    {
        public override ALogger Create(object owner)
        {
            return new UnityLogger(owner);
        }
    }
}
