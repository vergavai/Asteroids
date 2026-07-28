using System.Threading;
using Project.Code.Configs;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Movement
{
    public class PlayerMovement
    {
        private const float Epsilon = 0.0001f;
        
        private PlayerInput _input;
        private PlayerConfig _config;
        private PlayerInvincibility _invincibility;
        private Transform _transform;
        
        private Vector2 _velocity;          
        private Vector2 _externalVelocity;  
        private float _maxSpeed;
        private float _accelerationTime;
        private float _decelerationTime;
        private float _pushDecelerationTime; 

        public float CurrentSpeed => (_velocity + _externalVelocity).magnitude;
        public Vector2 CurrentVelocity => _velocity;
        
        public PlayerMovement(PlayerInput input, PlayerConfig config, PlayerInvincibility invincibility)
        {
            _input = input;
            _config = config;
            _accelerationTime = _config.AccelerationTime;
            _decelerationTime = _config.DecelerationTime;
            _maxSpeed = _config.MovementSpeed;
            _invincibility = invincibility;
            _velocity = Vector2.zero;
            _externalVelocity = Vector2.zero;
            _pushDecelerationTime = _config.PushDecelerationTime;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void AddImpulse(Vector2 impulse)
        {
            _externalVelocity += impulse;
        }

        public void UpdateMovement()
        {
            if (_externalVelocity.sqrMagnitude > Epsilon)
            {
                _externalVelocity = Vector2.Lerp(_externalVelocity, Vector2.zero, Time.deltaTime / _pushDecelerationTime);
                if (_externalVelocity.sqrMagnitude < Epsilon)
                    _externalVelocity = Vector2.zero;
            }

            Vector2 totalVelocity = _externalVelocity;

            if (!_invincibility.IsInvincible)
            {
                Vector2 inputDirection = new Vector2(_input.HorizontalInput, _input.VerticalInput);
                Vector2 targetVelocity = inputDirection.normalized * _maxSpeed; 
                float smoothTime = inputDirection.sqrMagnitude > Epsilon ? _accelerationTime : _decelerationTime;
                _velocity = Vector2.Lerp(_velocity, targetVelocity, Time.deltaTime / smoothTime);
                if (_velocity.sqrMagnitude < Epsilon) 
                    _velocity = Vector2.zero;

                totalVelocity += _velocity;
            }
            else
            {
                _velocity = Vector2.zero;
            }

            Vector2 movement = totalVelocity * Time.deltaTime;
            _transform.Translate(movement, Space.World);
        }
    }
}