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
            _enemy = asteroidShard;
        }

        public override void Kill()
        {
            gameObject.SetActive(false);
        }
    }
}