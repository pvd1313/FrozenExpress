using UnityEngine;

namespace FrozenExpress.Game.Player
{
    public class PlayerInstaller : AModuleInstaller
    {
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Install<PlayerViewFactory>(_playerViewPrefab);
            Install<PlayerController>(_playerConfig);
        }
    }
}
