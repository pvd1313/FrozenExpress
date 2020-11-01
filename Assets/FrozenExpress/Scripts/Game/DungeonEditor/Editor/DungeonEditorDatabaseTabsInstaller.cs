using Zenject;

namespace FrozenExpress.Game.DungeonEditor.Editor
{
    public class DungeonEditorDatabaseTabsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EntityPatternTab>().AsSingle();
            Container.BindInterfacesTo<RoomPatternTab>().AsSingle();
        }
    }
}