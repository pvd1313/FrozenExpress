using UnityEngine;

namespace FrozenExpress.Game.Cinematics
{
    public interface ICameraView
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
    }
}
