using Project.Code.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Gameplay.Enemies.Asteroid
{
    public class AsteroidShard : Enemy
    {
        private GameObject _self;
        private EnemyConfig _config;
        private Vector3 _direction;

        public AsteroidShard(EnemyConfig config)
        {
            _config = config;
        }

        public void Initialize(GameObject self)
        {
            _self = self;
            base.Initialize(self.transform);
        }

        public override void UpdatePosition()
        {
            base.UpdatePosition();

            _transform.Translate(_direction * (_config.AsteroidSpeed * Time.deltaTime));
        }

        public void OnAsteroidDestroyed(Transform asteroid)
        {
            _transform.position = asteroid.position;
            _self.SetActive(true);
            _direction = GetRandomDirection();
        }

        private Vector3 GetRandomDirection()
        {
            return Random.insideUnitCircle.normalized;
        }
    }
}