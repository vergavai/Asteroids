using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies.Saucer
{
    public class Saucer
    {
        private const float Epsilon = 0.0001f;
        
        private Transform _self;
        private Transform _player;
        private EnemyConfig _config;
        private Vector2 _targetPosition;
        private float _timer;

        private Vector2 _pushVelocity;

        public Saucer(EnemyConfig config)
        {
            _config = config;
        }

        public void Initialize(Transform self, Transform player)
        {
            _self = self;
            _player = player;
        }

        public void AddImpulse(Vector2 impulse)
        {
            _pushVelocity += impulse;
        }

        public void ResetState()
        {
            _timer = 0f;
            _targetPosition = _self.position;
            _pushVelocity = Vector2.zero;
        }

        public void UpdatePosition()
        {
            if (_pushVelocity.sqrMagnitude > Epsilon)
            {
                _pushVelocity = Vector2.Lerp(_pushVelocity, Vector2.zero, Time.deltaTime);
                if (_pushVelocity.sqrMagnitude < Epsilon)
                    _pushVelocity = Vector2.zero;

                _self.Translate(_pushVelocity * Time.deltaTime, Space.World);
            }

            if (!_player)
                return;

            _timer += Time.deltaTime;
            if (_timer >= _config.SaucerTrackInterval)
            {
                _timer = 0f;
                _targetPosition = _player.position;
            }

            Vector2 currentPos = _self.position;
            Vector2 direction = (_targetPosition - currentPos).normalized;
            float distance = Vector2.Distance(currentPos, _targetPosition);

            if (distance > Epsilon)
            {
                float moveDistance = _config.SaucerSpeed * Time.deltaTime;
                if (moveDistance > distance)
                    moveDistance = distance;

                _self.Translate(direction * moveDistance, Space.World);
            }
        }
    }
}