namespace FrozenExpress.Game.Logging
{
    public class LoggingInstaller : Zenject.ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ALogFactory>().To<UnityLogFactory>().AsSingle();
        }
    }
}
