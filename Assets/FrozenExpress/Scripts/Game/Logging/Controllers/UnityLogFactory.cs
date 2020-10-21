namespace FrozenExpress.Game.Logging
{
    public class UnityLogFactory : ALogFactory
    {
        public override ALogger Create(object owner)
        {
            return new UnityLogger(owner);
        }
    }
}
