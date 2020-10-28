using FrozenExpress.Game.GameCycle;
using FrozenExpress.Game.Player;

namespace FrozenExpress.Game.Cinematics
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
