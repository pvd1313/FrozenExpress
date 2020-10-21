using UnityEngine;

namespace FrozenExpress.Game.Cinematics
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
