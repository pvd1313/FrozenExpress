using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.Cinematics
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
