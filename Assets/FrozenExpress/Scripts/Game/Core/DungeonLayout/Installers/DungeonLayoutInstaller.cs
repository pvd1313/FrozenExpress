using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DungeonLayoutPatternCollectionAsset _dungeonLayoutPatternCollection;
        [SerializeField] private Vector3 _cellSize;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DungeonLayoutPatternMaster>().AsSingle().WithArguments(_dungeonLayoutPatternCollection.Patterns);
            Container.BindInterfacesTo<DungeonLayoutMeshBuilder>().AsSingle();
            Container.BindInterfacesTo<DungeonLayoutTransform>().AsSingle().WithArguments(_cellSize);
        }
    }
}
