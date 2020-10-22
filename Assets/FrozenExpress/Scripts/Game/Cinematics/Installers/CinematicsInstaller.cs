using FrozenExpress.Scripts.Game.Cinematics.Controllers;
using FrozenExpress.Scripts.Game.Cinematics.Views;
using FrozenExpress.Scripts.Game.DiFramework.Installers;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Cinematics.Installers
{
    public class CinematicsInstaller : AModuleInstaller
    {
        [SerializeField] private CameraView _cameraViewPrefab;

        public override void InstallBindings()
        {
            Spawn(_cameraViewPrefab);

            Install<CameraController>();
        }
    }
}
