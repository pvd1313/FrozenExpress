using UnityEngine;

namespace FrozenExpress.Game.Core.Player
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
