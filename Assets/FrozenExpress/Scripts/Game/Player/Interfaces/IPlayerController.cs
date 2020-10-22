using UnityEngine;

namespace FrozenExpress.Scripts.Game.Player.Interfaces
{
    public interface IPlayerController
    {
        Vector3 CameraPosition { get; }

        Quaternion CameraRotation { get; }
    }
}
