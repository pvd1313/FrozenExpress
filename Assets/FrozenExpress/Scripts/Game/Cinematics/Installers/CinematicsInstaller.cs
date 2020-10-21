using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.Core.Cinematics
{
    public class CinematicsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CameraView _cameraViewPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CameraView>().FromComponentsInNewPrefab(_cameraViewPrefab).AsSingle();

            Container.BindInterfacesTo<CameraController>().AsSingle();
        }
    }
}
