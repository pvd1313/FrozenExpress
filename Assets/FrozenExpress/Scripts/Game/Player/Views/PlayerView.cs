using UnityEngine;

namespace FrozenExpress.Game.Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private CharacterController _characterController;

        public Vector3 Position => transform.position;

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public float SkinWidth => _characterController.skinWidth;

        public float Height
        {
            get => _characterController.height;
            set
            {
                _characterController.height = value;
                _characterController.center = new Vector3(0, value / 2, 0);
            }
        }

        public float Radius
        {
            get => _characterController.radius;
            set => _characterController.radius = value;
        }

        public void Move(Vector3 motion)
        {
            _characterController.Move(motion);
        }

        public void Warp(Vector3 position)
        {
            transform.position = position;
        }
    }
}
