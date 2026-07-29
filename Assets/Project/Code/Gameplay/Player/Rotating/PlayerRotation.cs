using System;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Rotating
{
    public class PlayerRotation
    {
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

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void UpdateRotation()
        {
            if (_invincibility.IsInvincible) return;

            Vector2 aimDirection = _inputProvider.GetAimInput();
            if (aimDirection.sqrMagnitude < 0.01f) return;

            _direction = aimDirection;

            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, 0, angle);
            _angle = angle;
        }
    }
}