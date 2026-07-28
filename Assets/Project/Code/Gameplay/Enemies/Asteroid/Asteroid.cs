using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies.Asteroid
{
    public class Asteroid
    {
        private const float Epsilon = 0.0001f;
        
        private Transform _transform;
        private float _speed;
        private Vector3 _direction;
        private AsteroidShard[] _shards;

        private Vector2 _pushVelocity;

        public Asteroid(EnemyConfig config)
        {
            _speed = config.AsteroidSpeed;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void SetShards(AsteroidShard[] shards)
        {
            _shards = shards;
        }

        public void AddImpulse(Vector2 impulse)
        {
            _pushVelocity += impulse;
        }

        public void UpdatePosition()
        {
            if (_pushVelocity.sqrMagnitude > Epsilon)
            {
                _pushVelocity = Vector2.Lerp(_pushVelocity, Vector2.zero, Time.deltaTime);
                if (_pushVelocity.sqrMagnitude < Epsilon)
                    _pushVelocity = Vector2.zero;

                _transform.Translate(_pushVelocity * Time.deltaTime, Space.World);
            }

            _transform.Translate(_direction * (_speed * Time.deltaTime));
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public void OnAsteroidDestroyed()
        {
            ReleaseShards();
        }

        private void ReleaseShards()
        {
            foreach (var shard in _shards)
            {
                shard.OnAsteroidDestroyed(_transform);
            }
        }
    }
}