namespace FrozenExpress.Game.GameCycle
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
