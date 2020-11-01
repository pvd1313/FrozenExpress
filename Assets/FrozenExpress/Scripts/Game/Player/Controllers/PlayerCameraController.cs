using FrozenExpress.Game.Cinematics;
using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.Player
{
    public class PlayerCameraController : IPlayerCameraController, IInitializable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;
        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;
        
        private readonly IPlayerController _playerController;
        private readonly ICameraViewFactory _cameraViewFactory;

        private ICameraView _camera;
        private bool _enabled;

        public PlayerCameraController(IPlayerController playerController,
                                      ICameraViewFactory cameraViewFactory)
        {
            _playerController = playerController;
            _cameraViewFactory = cameraViewFactory;
        }
        
        public void Initialize()
        {
            _camera = _cameraViewFactory.CreateCamera();
            
            SetEnabled(true);
        }

        public void Update(FrameUpdateData frameData)
        {
            if (!_enabled)
            {
                return;
            }

            _camera.Position = _playerController.CameraPosition;
            _camera.Rotation = _playerController.CameraRotation;
        }
        
        public void SetEnabled(bool isEnabled)
        {
            _enabled = isEnabled;
            _camera.IsEnabled = isEnabled;
            Cursor.visible = !isEnabled;
            Cursor.lockState = isEnabled ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}