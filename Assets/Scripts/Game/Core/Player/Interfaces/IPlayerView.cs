using UnityEngine;

namespace FrozenExpress.Game.Core.Player
{
    public interface IPlayerView
    {
        Vector3 CameraPosition { get; }
        Quaternion CameraRotation { get; }
    }
}
