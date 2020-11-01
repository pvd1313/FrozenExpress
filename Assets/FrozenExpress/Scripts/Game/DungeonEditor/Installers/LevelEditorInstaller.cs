using FrozenExpress.Game.DiFramework.Installers;
using FrozenExpress.Game.DungeonEditor.Models;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor.Installers
{
    public class LevelEditorInstaller : AModuleInstaller
    {
        [SerializeField] private DungeonLayoutEditorCameraView _cameraPrefab;
        [SerializeField] private LevelEditorCameraConfig _cameraConfig;
        [SerializeField] private LevelDrawingConfig _drawingConfig;

        public override void InstallBindings()
        {
            Spawn(_cameraPrefab);

            Install<LevelEditorCameraController>(_cameraConfig);
            Install<LevelEditorModel>();
            Install<LevelDrawingController>(_drawingConfig);
        }
    }
}
