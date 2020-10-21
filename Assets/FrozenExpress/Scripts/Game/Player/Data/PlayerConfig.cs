using UnityEngine;

namespace FrozenExpress.Game.Core.Player
{
    [System.Serializable]
    public class PlayerConfig
    {
        public float MovementSpeed => _movementSpeed;
        public float CameraSensitivity => _cameraSensitivity;
        public float Gravity => _gravity;
        public float JumpHeight => _jumpHeight;

        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _cameraSensitivity;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight;
    }
}
