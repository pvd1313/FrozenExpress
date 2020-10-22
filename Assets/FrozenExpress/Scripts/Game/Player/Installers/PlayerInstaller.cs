using FrozenExpress.Scripts.Game.DiFramework.Installers;
using FrozenExpress.Scripts.Game.Player.Controllers;
using FrozenExpress.Scripts.Game.Player.Data;
using FrozenExpress.Scripts.Game.Player.Views;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Player.Installers
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
