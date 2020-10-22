using FrozenExpress.Scripts.Game.Player.Interfaces;
using FrozenExpress.Scripts.Game.Player.Views;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Player.Controllers
{
    public class PlayerViewFactory : IPlayerViewFactory
    {
        private readonly PlayerView _playerViewPrefab;

        public PlayerViewFactory(PlayerView playerViewPrefab)
        {
            _playerViewPrefab = playerViewPrefab;
        }

        public IPlayerView CreatePlayer()
        {
            return GameObject.Instantiate(_playerViewPrefab);
        }
    }
}
