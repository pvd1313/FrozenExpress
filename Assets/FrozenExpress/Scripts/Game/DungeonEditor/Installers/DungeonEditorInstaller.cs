using FrozenExpress.Game.DiFramework.Installers;
using FrozenExpress.Game.DungeonEditor.Models;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor.Installers
{
    public class DungeonEditorInstaller : AModuleInstaller
    {
        [SerializeField] private DungeonEditorCameraConfig _cameraConfig;
        [SerializeField] private DungeonEditorDrawingConfig _drawingConfig;

        public override void InstallBindings()
        {
            Install<DungeonEditorCameraController>(_cameraConfig);
            Install<DungeonEditorModel>();
            Install<DungeonEditorDrawingController>(_drawingConfig);
            Install<DungeonEditorModeController>();
        }
    }
}
