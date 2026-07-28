using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Enemies.Asteroid
{
    public class AsteroidShardBehaviour : EnemyBehaviour
    {
        private AsteroidShard _asteroidShard;
        public AsteroidShard AsteroidShard => _asteroidShard;

        [Inject]
        private void Construct(AsteroidShard asteroidShard)
        {
            _asteroidShard = asteroidShard;
            _asteroidShard.Initialize(gameObject);
        }

        public override void AddImpulse(Vector2 impulse)
        {
            _asteroidShard.AddImpulse(impulse);
        }

        private void Update()
        {
            _asteroidShard.UpdatePosition();
        }

        public override void Kill()
        {
            gameObject.SetActive(false);
        }
    }
}