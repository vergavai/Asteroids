using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies.Saucer
{
    public class Saucer : Enemy
    {
        private Transform _player;
        private EnemyConfig _config;

        private float _timer;
        private Vector2 _targetPosition;
        private Vector2 _pushVelocity;

        public Saucer(EnemyConfig config)
        {
            _config = config;
        }

        public void Initialize(Transform self, Transform player)
        {
            base.Initialize(self);
            _player = player;
        }

        public override void ResetState()
        {
            base.ResetState();
            _timer = 0f;
            _targetPosition = _transform.position;
        }

        public override void UpdatePosition()   
        {
            base.UpdatePosition();

            if (!_player)
                return;

            _timer += Time.deltaTime;
            if (_timer >= _config.SaucerTrackInterval)
            {
                _timer = 0f;
                _targetPosition = _player.position;
            }

            Vector2 currentPos = _transform.position;
            Vector2 direction = (_targetPosition - currentPos).normalized;
            float distance = Vector2.Distance(currentPos, _targetPosition);

            if (distance > Epsilon)
            {
                float moveDistance = _config.SaucerSpeed * Time.deltaTime;
                if (moveDistance > distance)
                    moveDistance = distance;

                _transform.Translate(direction * moveDistance, Space.World);
            }
        }
    }
}