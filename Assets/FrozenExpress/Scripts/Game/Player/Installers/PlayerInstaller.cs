using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.Player
{
    public class PlayerInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerSpawnPointView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesTo<PlayerViewFactory>().AsSingle().WithArguments(_playerViewPrefab);
            Container.BindInterfacesTo<PlayerController>().AsSingle().WithArguments(_playerConfig);
        }
    }
}
