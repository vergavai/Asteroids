using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Rotating
{
    public class PlayerRotation
    {
        private const float Epsilon = 0.01f;
        
        private Transform _transform;
        private IInputProvider _inputProvider;
        private PlayerInvincibility _invincibility;

        private float _angle;
        private Vector2 _direction;

        public float Angle => _angle;
        public Vector2 Direction => _direction;

        public PlayerRotation(IInputProvider inputProvider, PlayerInvincibility invincibility)
        {
            _inputProvider = inputProvider;
            _invincibility = invincibility;
            _direction = Vector2.right;
        }

        public void Initialize(Transform transform) => _transform = transform;

        public void UpdateRotation()
        {
            if (_invincibility.IsInvincible) return;

            Vector2 targetPosition = _inputProvider.GetAimTarget(_transform.position);
            Vector2 direction = (targetPosition - (Vector2)_transform.position).normalized;

            if (direction.sqrMagnitude < Epsilon) return;

            _direction = direction;
            _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}