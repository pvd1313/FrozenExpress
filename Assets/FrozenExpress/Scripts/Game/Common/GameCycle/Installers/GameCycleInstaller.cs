using Zenject;

namespace FrozenExpress.Game.Common.GameCycle
{
    public class GameCycleInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UpdateMediatorView>().FromNew();

            Container.BindInterfacesTo<InitializationMediator>().AsSingle();
            Container.BindInterfacesTo<TerminationMediator>().AsSingle();
            Container.BindInterfacesTo<FrameUpdateMediator>().AsSingle();
        }
    }
}
