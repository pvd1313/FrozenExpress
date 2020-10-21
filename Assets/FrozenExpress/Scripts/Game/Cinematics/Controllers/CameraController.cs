using FrozenExpress.Game.Common.GameCycle;
using FrozenExpress.Game.Core.Player;

namespace FrozenExpress.Game.Core.Cinematics
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
