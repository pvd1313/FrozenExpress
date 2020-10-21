using FrozenExpress.Game.DungeonLayout;
using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonLayoutInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DungeonPatternDatabaseAsset _patternDatabase;
        [SerializeField] private Vector3 _cellSize;
        [SerializeField] private DungeonEntityDatabaseAsset _entityDatabase;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DungeonEntityDatabase>().AsSingle().WithArguments(_entityDatabase);
            Container.BindInterfacesTo<MeshBuilder>().AsTransient();
            Container.BindInterfacesTo<DungeonPatternController>().AsSingle().WithArguments(_patternDatabase.Patterns);
            Container.BindInterfacesTo<DungeonPatternTransformController>().AsSingle();
            Container.BindInterfacesTo<DungeonMeshBuilder>().AsSingle();
            Container.BindInterfacesTo<DungeonTransform>().AsSingle().WithArguments(_cellSize);
            Container.BindInterfacesTo<DungeonPatternExpressionParser>().AsSingle();
        }
    }
}
