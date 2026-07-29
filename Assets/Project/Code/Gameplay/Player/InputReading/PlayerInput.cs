using System;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using UnityEngine;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class PlayerInput
    {
        private IInputProvider _inputProvider;
        private PlayerInvincibility _invincibility;
        
        private float _horizontalInput;
        private float _verticalInput;

        public event Action ShootRequested;
        public event Action LaserRequested;

        public float HorizontalInput => _horizontalInput;
        public float VerticalInput => _verticalInput;

        public PlayerInput(IInputProvider inputProvider, PlayerInvincibility invincibility)
        {
            _inputProvider = inputProvider;
            _invincibility = invincibility;

            _inputProvider.ShootPerformed += () => ShootRequested?.Invoke();
            _inputProvider.LaserPerformed += () => LaserRequested?.Invoke();
        }

        public void UpdateInput()
        {
            if (_invincibility.IsInvincible)
            {
                _horizontalInput = 0;
                _verticalInput = 0;
                return;
            }

            _inputProvider.UpdateInput();
            
            Vector2 movement = _inputProvider.GetMovementInput();
            _horizontalInput = movement.x;
            _verticalInput = movement.y;
        }
    }
}