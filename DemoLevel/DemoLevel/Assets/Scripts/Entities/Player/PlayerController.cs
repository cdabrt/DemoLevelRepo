using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private float jumpForce = 40f;
        
        [SerializeField] private float graviticConstant = 9.81f;
        private float _gravity;
        
        [SerializeField] private Transform playerCamera;
        
        private CharacterController _characterController;
        private Vector2 _movement;
        private float _vertical;
        private const float RotationFactor = 0.2f;

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _xRotation = transform.rotation.x;
            _yRotation = transform.rotation.y;
            _vertical = 0f;
            _gravity = 0f;
        }

        public void Update()
        {
            ApplyGravity();
            
            Rotate();
            _characterController.Move(transform.TransformDirection(
                new Vector3(_movement.x, _vertical, _movement.y)
                ) * Time.deltaTime);
        }
        
        private void ApplyGravity()
        {
            if (_characterController.isGrounded)
            {
                _gravity = 0f;
            }
            else
            {
                //As time goes on, object will fall quicker.
                _gravity = graviticConstant * Time.deltaTime;
                _vertical -= _gravity;
            }
        }

        private void Rotate()
        {
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(0.0f, _yRotation, 0.0f);
            playerCamera.rotation = Quaternion.Euler(_xRotation, _yRotation, 0.0f);
        }

        
        
        private void OnLook(InputValue value)
        {
            var vector = value.Get<Vector2>();
            _xRotation += -vector.y * RotationFactor * rotationSpeed * Time.deltaTime;
            _yRotation += vector.x * RotationFactor * rotationSpeed * Time.deltaTime;;
        }

        private void OnMove(InputValue value)
        {
            var vector = value.Get<Vector2>();
            _movement = new Vector2(vector.x, vector.y).normalized;
            _movement *= movementSpeed;
        }

        private void OnJump()
        {
            if (_characterController.isGrounded)
            {
                _vertical = jumpForce;   
            }
        }
    }
}