using FrozenExpress.Game.DiFramework.Installers;
using UnityEngine;

namespace FrozenExpress.Game.Cinematics.Installers
{
    public class CinematicsInstaller : AModuleInstaller
    {
        [SerializeField] private CameraView _cameraViewPrefab;

        public override void InstallBindings()
        {
            Install<CameraViewFactory>(_cameraViewPrefab);
        }
    }
}
