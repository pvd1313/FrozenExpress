using Zenject;

namespace FrozenExpress.Game.DungeonEditor.Editor
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