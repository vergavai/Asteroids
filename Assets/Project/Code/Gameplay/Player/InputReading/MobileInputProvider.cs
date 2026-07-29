using System;
using UnityEngine;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class MobileInputProvider : IInputProvider
    {
        private Vector2 _movementInput;
        private Vector2 _aimInput;
        private bool _shootPressed;
        private bool _laserPressed;

        public event Action ShootPerformed;
        public event Action LaserPerformed;

        public void SetMovementInput(Vector2 value) => _movementInput = value;
        public void SetAimInput(Vector2 value) => _aimInput = value;

        public void SetShootPressed()
        {
            ShootPerformed?.Invoke();
        }

        public void SetLaserPressed()
        {
            LaserPerformed?.Invoke();
        }

        public void UpdateInput() { }

        public Vector2 GetMovementInput() => _movementInput;
        public Vector2 GetAimInput() => _aimInput;
    }
}