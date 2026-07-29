using System;
using UnityEngine;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class DesktopInputProvider : IInputProvider
    {
        private Camera _camera;
        private Transform _playerTransform;
        private Vector2 _movementInput;
        private Vector2 _aimInput;

        public event Action ShootPerformed;
        public event Action LaserPerformed;

        public DesktopInputProvider(Camera camera)
        {
            _camera = camera;
        }

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void UpdateInput()
        {
            _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - _playerTransform.position).normalized;
            _aimInput = direction;

            if (Input.GetMouseButtonDown(0))
                ShootPerformed?.Invoke();

            if (Input.GetMouseButtonDown(1))
                LaserPerformed?.Invoke();
        }

        public Vector2 GetMovementInput() => _movementInput;
        public Vector2 GetAimInput() => _aimInput;
    }
}