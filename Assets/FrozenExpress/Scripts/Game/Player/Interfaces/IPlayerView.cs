using UnityEngine;

namespace FrozenExpress.Scripts.Game.Player.Interfaces
{
    public interface IPlayerView
    {
        Vector3 Position { get; set; }

        Quaternion Rotation { get; set; }

        float Radius { get; set; }

        float Height { get; set; }

        float SkinWidth { get; }

        void Move(Vector3 motion);
    }
}
