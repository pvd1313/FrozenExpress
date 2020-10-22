using FrozenExpress.Scripts.Game.DiFramework.Installers;
using FrozenExpress.Scripts.Game.Dungeon.Assets;
using FrozenExpress.Scripts.Game.Dungeon.Controllers;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Installers
{
    public class DungeonLayoutInstaller : AModuleInstaller
    {
        [SerializeField] private DungeonPatternDatabaseAsset _patternDatabase;
        [SerializeField] private Vector3 _cellSize;
        [SerializeField] private DungeonEntityDatabaseAsset _entityDatabase;

        public override void InstallBindings()
        {
            Install<DungeonEntityDatabase>(_entityDatabase);
            Install<MeshBuilder>();
            Install<DungeonPatternController>(_patternDatabase.Patterns);
            Install<DungeonPatternTransformController>();
            Install<DungeonMeshBuilder>();
            Install<DungeonTransform>(_cellSize);
            Install<DungeonPatternExpressionParser>();
        }
    }
}
