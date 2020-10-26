using Zenject;

namespace FrozenExpress.Scripts.Game.LevelEditor.Editor
{
    public class LevelEditorDatabaseTabsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EntityPatternTab>().AsSingle();
            Container.BindInterfacesTo<RoomPatternTab>().AsSingle();
        }
    }
}