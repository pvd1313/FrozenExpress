using FrozenExpress.Game.DiFramework.Installers;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon.Installers
{
    public class DungeonLayoutInstaller : AModuleInstaller
    {
        [SerializeField] private DungeonEntityPatternDatabaseAsset _entityPatternDatabase;
        [SerializeField] private Vector3 _cellSize;
        [SerializeField] private DungeonEntityDatabaseAsset _entityDatabase;

        public override void InstallBindings()
        {
            Install<DungeonEntityDatabase>(_entityDatabase);
            Install<MeshBuilder>();
            Install<DungeonPatternController>();
            Install<DungeonPatternTransformController>();
            Install<DungeonMeshBuilder>();
            Install<DungeonTransform>(_cellSize);
            Install<DungeonPatternExpressionParser>();
            Install<DungeonEntityPatternDatabase>(_entityPatternDatabase);
        }
    }
}
