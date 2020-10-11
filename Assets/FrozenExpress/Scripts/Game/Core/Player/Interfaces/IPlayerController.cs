using UnityEngine;

namespace FrozenExpress.Game.Core.Player
{
    public interface IPlayerController
    {
        Vector3 CameraPosition { get; }

        Quaternion CameraRotation { get; }
    }
}
