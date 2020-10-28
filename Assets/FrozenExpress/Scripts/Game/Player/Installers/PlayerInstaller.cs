using FrozenExpress.Game.DiFramework.Installers;
using UnityEngine;

namespace FrozenExpress.Game.Player.Installers
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
