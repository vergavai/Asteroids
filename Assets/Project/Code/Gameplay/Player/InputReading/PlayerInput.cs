using Project.Code.Gameplay.Player.Collisions.Invincibility;
using UnityEngine;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class PlayerInput
    {
        private IInputProvider _inputProvider;
        private PlayerInvincibility _invincibility;
        private bool _isMoving;

        private float _horizontalInput;
        private float _verticalInput;

        public float HorizontalInput => _horizontalInput;
        public float VerticalInput => _verticalInput;

        public PlayerInput(IInputProvider inputProvider, PlayerInvincibility invincibility)
        {
            _inputProvider = inputProvider;
            _invincibility = invincibility;
        }

        public void UpdateInput()
        {
            _inputProvider.UpdateInput();

            if (_invincibility.IsInvincible)
            {
                _horizontalInput = 0;
                _verticalInput = 0;
                return;
            }

            Vector2 movement = _inputProvider.GetMovementInput();
            _horizontalInput = movement.x;
            _verticalInput = movement.y;
        }
    }
}