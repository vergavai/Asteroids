using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Enemies.Asteroid
{
    public class AsteroidBehaviour : EnemyBehaviour
    {
        private Asteroid _asteroid;

        [Inject]
        private void Construct(Asteroid asteroid)
        {
            _asteroid = asteroid;
            _asteroid.Initialize(transform);
        }

        public override void AddImpulse(Vector2 impulse)
        {
            _asteroid.AddImpulse(impulse);
        }

        public void SetShards(AsteroidShardBehaviour[] shardBehaviours)
        {
            AsteroidShard[] shards = new AsteroidShard[shardBehaviours.Length];
            for (int i = 0; i < shards.Length; i++)
            {
                shards[i] = shardBehaviours[i].AsteroidShard;
            }
            _asteroid.SetShards(shards);
        }

        private void Update()
        {
            _asteroid.UpdatePosition();
        }

        public void SetDirection(Vector3 direction)
        {
            _asteroid.SetDirection(direction);
        }

        public override void Kill()
        {
            _asteroid.OnAsteroidDestroyed();
            gameObject.SetActive(false);
        }
    }
}