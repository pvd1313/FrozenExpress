using FrozenExpress.Game.DiFramework.Installers;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public class DungeonGeneratorInstaller : AModuleInstaller
    {
        [SerializeField] private DungeonRoomPatternDatabaseAsset _roomDatabase;
        [SerializeField] private DungeonLayoutGeneratorConfig _generatorConfig;
        
        public override void InstallBindings()
        {
            Install<DungeonRoomPatternProcessor>();
            Install<DungeonRoomPatternDatabase>(_roomDatabase);
            Install<DungeonLayoutGenerator>(_generatorConfig);
        }
    }
}