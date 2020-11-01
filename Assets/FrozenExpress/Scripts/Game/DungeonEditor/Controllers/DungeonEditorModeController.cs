using FrozenExpress.Game.Dungeon;
using FrozenExpress.Game.GameCycle;
using FrozenExpress.Game.Player;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor
{
    public class DungeonEditorModeController : IInitializable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.CameraModeController;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Early;
        
        private readonly IPlayerController _playerController;
        private readonly IPlayerCameraController _playerCameraController;
        private readonly IDungeonEditorCameraController _editorCameraController;
        private readonly IDungeonEditorModel _dungeonEditorModel;
        private readonly IDungeonTransform _dungeonTransform;

        private bool _isEditorMode;

        public DungeonEditorModeController(IPlayerController playerController,
                                           IPlayerCameraController playerCameraController,
                                           IDungeonEditorCameraController editorCameraController,
                                           IDungeonEditorModel dungeonEditorModel,
                                           IDungeonTransform dungeonTransform)
        {
            _playerController = playerController;
            _playerCameraController = playerCameraController;
            _editorCameraController = editorCameraController;
            _dungeonEditorModel = dungeonEditorModel;
            _dungeonTransform = dungeonTransform;
        }
        
        public void Initialize()
        {
            SwitchMode(true);
        }
        
        public void Update(FrameUpdateData frameData)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchMode(!_isEditorMode);
            }
        }

        private void SwitchMode(bool isEditorMode)
        {
            _isEditorMode = isEditorMode;
            
            _playerController.SetEnabled(!_isEditorMode);
            _playerCameraController.SetEnabled(!_isEditorMode);
            _editorCameraController.SetEnabled(_isEditorMode);

            if (!_isEditorMode)
            {
                var position = _dungeonTransform.TransformPosition(_dungeonEditorModel.CursorPosition);
                _playerController.Warp(position);
            }
        }
    }
}