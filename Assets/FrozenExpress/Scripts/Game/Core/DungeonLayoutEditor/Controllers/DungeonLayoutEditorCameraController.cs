using FrozenExpress.Game.Common.GameCycle;
using FrozenExpress.Game.Core.DungeonLayout;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayoutEditor
{
    public class DungeonLayoutEditorCameraController : IFrameUpdatable
    {
        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Early;

        private readonly IDungeonLayoutEditorCameraView _cameraView;
        private readonly DungeonLayoutEditorCameraControllerConfig _config;
        private readonly IDungeonLayoutEditorModel _dungeonModel;
        private readonly IDungeonTransform _dungeonLayoutTransform;

        private Vector2 _cameraRotation;

        public DungeonLayoutEditorCameraController(IDungeonLayoutEditorCameraView cameraView,
                                                   DungeonLayoutEditorCameraControllerConfig config,
                                                   IDungeonLayoutEditorModel dungeonModel,
                                                   IDungeonTransform dungeonLayoutTransform)
        {
            _cameraView = cameraView;
            _config = config;
            _dungeonModel = dungeonModel;
            _dungeonLayoutTransform = dungeonLayoutTransform;
        }

        public void Update(FrameUpdateData frameData)
        {
            if (Input.GetMouseButton(1))
            {
                _cameraRotation.x -= Input.GetAxis("Mouse Y") * _config.Sensitivity;
                _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, 0, 90);

                _cameraRotation.y += Input.GetAxis("Mouse X") * _config.Sensitivity;
                _cameraRotation.y = Mathf.Repeat(_cameraRotation.y, 360);
            }

            var cameraRotation = Quaternion.Euler(_cameraRotation.x, _cameraRotation.y, 0);
            _cameraView.Rotation = cameraRotation;

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

            _cameraView.Position = _dungeonLayoutTransform.TransformPosition(_dungeonModel.CursorPosition);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_dungeonModel.CheckCellExists(_dungeonModel.CursorPosition))
                {
                    _dungeonModel.RemoveCell(_dungeonModel.CursorPosition);
                }
                else
                {
                    _dungeonModel.AddCell(_dungeonModel.CursorPosition);
                }
            }
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
