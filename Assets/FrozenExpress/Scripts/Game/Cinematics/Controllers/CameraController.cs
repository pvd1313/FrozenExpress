using FrozenExpress.Scripts.Game.Cinematics.Interfaces;
using FrozenExpress.Scripts.Game.GameCycle.Data;
using FrozenExpress.Scripts.Game.GameCycle.Enums;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;
using FrozenExpress.Scripts.Game.Player.Interfaces;

namespace FrozenExpress.Scripts.Game.Cinematics.Controllers
{
    public class CameraController : IFrameUpdatable
    {
        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly ICameraView _cameraView;
        private readonly IPlayerController _playerController;

        public CameraController(ICameraView cameraView,
                                IPlayerController playerController)
        {
            _cameraView = cameraView;
            _playerController = playerController;
        }

        public void Update(FrameUpdateData frameData)
        {
            _cameraView.Position = _playerController.CameraPosition;
            _cameraView.Rotation = _playerController.CameraRotation;
        }
    }
}
