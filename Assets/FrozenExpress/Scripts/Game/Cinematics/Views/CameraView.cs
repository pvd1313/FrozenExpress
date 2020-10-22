using FrozenExpress.Scripts.Game.Cinematics.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Cinematics.Views
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
    }
}
