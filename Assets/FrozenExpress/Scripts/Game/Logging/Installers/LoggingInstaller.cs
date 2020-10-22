using FrozenExpress.Scripts.Game.Logging.Controllers;

namespace FrozenExpress.Scripts.Game.Logging.Installers
{
    public class LoggingInstaller : Zenject.ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ALogFactory>().To<UnityLogFactory>().AsSingle();
        }
    }
}
