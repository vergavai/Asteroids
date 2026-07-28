using System;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Rotating
{
    public class PlayerRotation
    {
        private float _angle;
        private Transform _transform;
        private Camera _camera;
        private PlayerInvincibility _invincibility;
        
        public float Angle => _angle;

        public PlayerRotation(Camera camera, PlayerInvincibility invincibility)
        {
            _camera = camera;
            _invincibility = invincibility;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }
        
        public void UpdateRotation()
        {
            if(_invincibility.IsInvincible) return;
            
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPos - _transform.position).normalized;

            _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}
