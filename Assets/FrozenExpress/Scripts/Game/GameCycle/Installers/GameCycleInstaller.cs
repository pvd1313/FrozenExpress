using FrozenExpress.Scripts.Game.GameCycle.Controllers;
using FrozenExpress.Scripts.Game.GameCycle.Views;

namespace FrozenExpress.Scripts.Game.GameCycle.Installers
{
    public class GameCycleInstaller : Zenject.ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCycleMediatorView>().FromNewComponentOnRoot().AsSingle();

            Container.BindInterfacesTo<InitializationMediator>().AsSingle();
            Container.BindInterfacesTo<TerminationMediator>().AsSingle();
            Container.BindInterfacesTo<FrameUpdateMediator>().AsSingle();
        }
    }
}
