using FrozenExpress.Game.Cinematics;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Dungeon;
using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor
{
    public class DungeonEditorCameraController : IFrameUpdatable, IInitializable, IDungeonEditorCameraController
    {
        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Early;
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;
        
        private readonly ICameraViewFactory _cameraViewFactory;
        private readonly DungeonEditorCameraConfig _config;
        private readonly IDungeonEditorModel _dungeonModel;
        private readonly IDungeonTransform _dungeonLayoutTransform;
        private readonly ADebugDrawer _pivotDrawer;

        private ICameraView _camera;
        private Vector2 _rotation;
        private float _offset;
        private bool _isEnabled;

        public DungeonEditorCameraController(ICameraViewFactory cameraViewFactory,
                                           DungeonEditorCameraConfig config,
                                           IDungeonEditorModel dungeonModel,
                                           IDungeonTransform dungeonLayoutTransform,
                                           ADebugFactory debugFactory)
        {
            _cameraViewFactory = cameraViewFactory;
            _config = config;
            _dungeonModel = dungeonModel;
            _dungeonLayoutTransform = dungeonLayoutTransform;
            _offset = 10;
            
            _pivotDrawer = debugFactory.CreateDrawer(this);
            _pivotDrawer?.SetShape(EDebugShape.Sphere3D);
            _pivotDrawer?.SetColor(EDebugColor.Yellow);
        }
        
        public void Initialize()
        {
            _camera = _cameraViewFactory.CreateCamera();
            _isEnabled = true;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            _camera.IsEnabled = isEnabled;
        }

        public void Update(FrameUpdateData frameData)
        {
            if (!_isEnabled)
            {
                return;
            }
            
            HandleInputActions();
            
            var pivotPosition = _dungeonLayoutTransform.TransformPosition(_dungeonModel.CursorPosition);
            var pivotRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
            
            _camera.Position = pivotPosition - pivotRotation * (Vector3.forward * _offset);
            _camera.Rotation = pivotRotation;
            _pivotDrawer?.Draw(pivotPosition);

            if (CheckNumpadKeyDown(out var keyValue))
            {
                var position = _dungeonModel.CursorPosition;
                if (keyValue == 0)
                {
                    _dungeonModel.SetCellLayer(position, 0);
                    return;
                }
                
                var mapLayer = _dungeonModel.GetCellLayer(position);
                _dungeonModel.SetCellLayer(position, mapLayer ^ (1 << (keyValue - 1)));
            }
        }

        private void HandleInputActions()
        {
            if (Input.GetMouseButton(1))
            {
                _rotation.x -= Input.GetAxis("Mouse Y") * _config.Sensitivity;
                _rotation.x = Mathf.Clamp(_rotation.x, 0, 90);

                _rotation.y += Input.GetAxis("Mouse X") * _config.Sensitivity;
                _rotation.y = Mathf.Repeat(_rotation.y, 360);
            }
            
            var cameraRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
            var viewForward = ComputeForwardDirection(cameraRotation);
            var viewRight = ComputeRightDirection(cameraRotation);
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveCursor(viewForward);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveCursor(-viewForward);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveCursor(viewRight);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveCursor(-viewRight);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveCursor(Vector3Int.up);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                MoveCursor(Vector3Int.down);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _dungeonModel.Clear();
            }
        }

        private bool CheckNumpadKeyDown(out int keyValue)
        {
            keyValue = 0;
            for (var key = KeyCode.Keypad0; key <= KeyCode.Keypad9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    keyValue = key - KeyCode.Keypad0;
                    return true;
                }
            }

            return false;
        }

        private void MoveCursor(Vector3Int delta)
        {
            _dungeonModel.CursorPosition += delta;
        }

        private Vector3Int ComputeForwardDirection(Quaternion viewRotation)
        {
            var direction = Vector3.ProjectOnPlane(viewRotation * Vector3.forward, Vector3.up);
            return ConvertDirection(direction);
        }

        private Vector3Int ComputeRightDirection(Quaternion viewRotation)
        {
            var direction = Vector3.ProjectOnPlane(viewRotation * Vector3.forward, Vector3.up);
            direction = Vector3.Cross(Vector3.up, direction);
            return ConvertDirection(direction);
        }

        private Vector3Int ConvertDirection(Vector3 direction)
        {
            var zAxis = new Vector3Int(0, 0, (int)Mathf.Sign(direction.z));
            var xAxis = new Vector3Int((int)Mathf.Sign(direction.x), 0, 0);

            return Mathf.Abs(direction.z) > Mathf.Abs(direction.x) ? zAxis : xAxis;
        }
    }
}
