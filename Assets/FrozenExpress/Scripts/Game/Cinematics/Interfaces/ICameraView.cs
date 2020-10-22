using UnityEngine;

namespace FrozenExpress.Scripts.Game.Cinematics.Interfaces
{
    public interface ICameraView
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
    }
}
