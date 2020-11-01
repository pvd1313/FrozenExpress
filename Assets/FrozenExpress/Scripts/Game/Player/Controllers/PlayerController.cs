using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.Player
{
    public class PlayerController : IPlayerController, IInitializable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Early;

        public Vector3 CameraPosition { get; private set; }

        public Quaternion CameraRotation { get; private set; }

        private readonly IPlayerViewFactory _playerViewFactory;
        private readonly PlayerConfig _playerConfig;
        private readonly Collider[] _colliderBuffer;

        private Vector2 _rotation;
        private IPlayerView _playerView;
        private float _velocityY;
        private bool _isEnabled;

        public PlayerController(IPlayerViewFactory playerViewFactory,
                                PlayerConfig playerConfig)
        {
            _playerViewFactory = playerViewFactory;
            _playerConfig = playerConfig;
            _colliderBuffer = new Collider[4];
        }

        public void Initialize()
        {
            _playerView = _playerViewFactory.CreatePlayer();
            _isEnabled = true;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public void Warp(Vector3 position)
        {
            _playerView.Warp(position);
        }

        public void Update(FrameUpdateData frameData)
        {
            if (!_isEnabled)
            {
                return;
            }
            
            _rotation.x -= Input.GetAxis("Mouse Y") * _playerConfig.CameraSensitivity;
            _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);
            _rotation.y += Input.GetAxis("Mouse X") * _playerConfig.CameraSensitivity;
            _rotation.y = Mathf.Repeat(_rotation.y, 360);

            CameraRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);

            var forward = Vector3.forward * Input.GetAxis("Vertical");
            var right = Vector3.right * Input.GetAxis("Horizontal");
            var motion = Quaternion.Euler(0, _rotation.y, 0) * (forward + right).normalized * _playerConfig.MovementSpeed;
            _playerView.Move(motion * frameData.DeltaTime);

            var castPoint = _playerView.Position;
            castPoint.y += _playerView.Radius - _playerView.SkinWidth;

            Debug.DrawLine(castPoint, castPoint - Vector3.up * _playerView.Radius);

            var colliderCount = Physics.OverlapSphereNonAlloc(castPoint, _playerView.Radius, _colliderBuffer);
            var isGrounded = colliderCount > 1;

            _velocityY -= _playerConfig.Gravity * frameData.DeltaTime;
            if (_velocityY < 0 && isGrounded)
            {
                _velocityY = 0;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                _velocityY = Mathf.Sqrt(2 * _playerConfig.JumpHeight * _playerConfig.Gravity);
            }

            _playerView.Move(new Vector3(0, _velocityY, 0) * frameData.DeltaTime);

            var cameraPos = _playerView.Position;
            cameraPos.y += _playerView.Height;
            CameraPosition = cameraPos;
        }
    }
}
