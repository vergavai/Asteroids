using Project.Code.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Gameplay.Enemies.Asteroid
{
    public class AsteroidShard
    {
        private const float Epsilon = 0.0001f;
        
        private GameObject _self;
        private Transform _transform;
        private EnemyConfig _config;
        private Vector3 _direction;

        private Vector2 _pushVelocity;

        public AsteroidShard(EnemyConfig config)
        {
            _config = config;
        }

        public void Initialize(GameObject self)
        {
            _self = self;
            _transform = _self.transform;
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

            _transform.Translate(_direction * (_config.AsteroidSpeed * Time.deltaTime));
        }

        public void OnAsteroidDestroyed(Transform asteroid)
        {
            _transform.position = asteroid.position;
            _self.SetActive(true);
            _direction = GetRandomDirection();
            _pushVelocity = Vector2.zero;
        }

        private Vector3 GetRandomDirection()
        {
            return Random.insideUnitCircle.normalized;
        }
    }
}