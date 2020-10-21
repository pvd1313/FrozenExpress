using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DungeonPatternDatabase _patternDatabase;
        [SerializeField] private Vector3 _cellSize;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DungeonPatternController>().AsSingle().WithArguments(_patternDatabase.Patterns);
            Container.BindInterfacesTo<DungeonPatternTransformController>().AsSingle();
            Container.BindInterfacesTo<DungeonMeshBuilder>().AsSingle();
            Container.BindInterfacesTo<DungeonTransform>().AsSingle().WithArguments(_cellSize);
            Container.BindInterfacesTo<DungeonPatternExpressionParser>().AsSingle();
        }
    }
}
