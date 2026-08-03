using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies.Asteroid
{
    public class Asteroid : Enemy
    {
        private float _speed;
        private Vector3 _direction;
        private AsteroidShard[] _shards;

        private Vector2 _pushVelocity;

        public Asteroid(EnemyConfig config)
        {
            _speed = config.AsteroidSpeed;
        }

        public void SetShards(AsteroidShard[] shards)
        {
            _shards = shards;
        }

        public override void UpdatePosition()
        {
            base.UpdatePosition();

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