using Boo.Lang;
using UnityEngine;

namespace FrozenExpress.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private float _cameraSensitivity = 5;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _gravity = 9.81f;
        [SerializeField] private float _jumpHeight = 0.5f;

        private float _cameraRotationY;
        private float _velocityY;
        private Collider[] _colliderBuffer = new Collider[4];

        private void Update()
        {
            var forwardAxis = Input.GetAxis("Vertical");
            var sizeAxis = Input.GetAxis("Horizontal");

            var forwardMotion = Vector3.forward * forwardAxis;
            var sideMotion = Vector3.right * sizeAxis;
            var motion = _bodyTransform.rotation * (forwardMotion + sideMotion).normalized * _movementSpeed * Time.deltaTime;

            var castPoint = _bodyTransform.position;
            castPoint.y += _characterController.center.y - _characterController.height / 2 + _characterController.radius / 2;
            var colliderCount = Physics.OverlapSphereNonAlloc(castPoint, _characterController.radius, _colliderBuffer);
            var grounded = colliderCount > 1;

            _velocityY -= _gravity * Time.deltaTime;

            if (_velocityY < 0 && grounded)
            {
                _velocityY = 0;
            }

            if (Input.GetButtonDown("Jump") && grounded)
            {
                 _velocityY = Mathf.Sqrt(2 * _jumpHeight * _gravity);
            }

            motion.y += _velocityY * Time.deltaTime;

            _characterController.Move(motion);

            _bodyTransform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _cameraSensitivity);

            _cameraRotationY += Input.GetAxis("Mouse Y") * _cameraSensitivity;
            _cameraRotationY = Mathf.Clamp(_cameraRotationY, -90, 90);
             _cameraTransform.localRotation = Quaternion.Euler(-_cameraRotationY, 0, 0);
        }
    }
}
