using FrozenExpress.Game.DiFramework.Installers;
using FrozenExpress.Game.DungeonEditor.Models;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor.Installers
{
    public class LevelEditorInstaller : AModuleInstaller
    {
        [SerializeField] private LevelEditorCameraConfig _cameraConfig;
        [SerializeField] private LevelDrawingConfig _drawingConfig;

        public override void InstallBindings()
        {
            Install<LevelEditorCameraController>(_cameraConfig);
            Install<LevelEditorModel>();
            Install<LevelDrawingController>(_drawingConfig);
        }
    }
}
