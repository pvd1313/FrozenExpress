using FrozenExpress.Scripts.Game.DiFramework.Installers;
using FrozenExpress.Scripts.Game.LevelEditor.Controllers;
using FrozenExpress.Scripts.Game.LevelEditor.Data;
using FrozenExpress.Scripts.Game.LevelEditor.Models;
using FrozenExpress.Scripts.Game.LevelEditor.Views;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Installers
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
