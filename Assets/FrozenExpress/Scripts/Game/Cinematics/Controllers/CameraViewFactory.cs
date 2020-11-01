using System.Runtime.InteropServices;
using UnityEngine;

namespace FrozenExpress.Game.Cinematics
{
    public class CameraViewFactory : ICameraViewFactory
    {
        private readonly CameraView _prefab;

        public CameraViewFactory(CameraView prefab)
        {
            _prefab = prefab;
        }
        
        public ICameraView CreateCamera()
        {
            return GameObject.Instantiate(_prefab);
        }
    }
}