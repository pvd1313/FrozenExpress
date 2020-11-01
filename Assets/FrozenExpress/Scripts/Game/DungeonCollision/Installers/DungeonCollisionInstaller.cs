using FrozenExpress.Game.DiFramework.Installers;

namespace FrozenExpress.Game.DungeonCollision
{
    public class DungeonCollisionInstaller : AModuleInstaller
    {
        public override void InstallBindings()
        {
            Install<DungeonCollisionController>();
        }
    }
}