using UnityEngine;

namespace FrozenExpress.Game.Player
{
    public interface IPlayerController
    {
        Vector3 CameraPosition { get; }

        Quaternion CameraRotation { get; }

        void Warp(Vector3 position);
        
        void SetEnabled(bool isEnabled);
    }
}
