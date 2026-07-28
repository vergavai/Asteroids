using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Collisions.Detector;
using Project.Code.Gameplay.Player.Movement;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Collisions.Pusher
{
    public class PlayerPusher
    {
        private Transform _transform;
        private PlayerMovement _playerMovement; 
        private PlayerCollisionDetector _collisionDetector;
        private PlayerConfig _config;

        public PlayerPusher(PlayerCollisionDetector collisionDetector, PlayerMovement playerMovement, PlayerConfig config)
        {
            _collisionDetector = collisionDetector;
            _playerMovement = playerMovement;
            _config = config;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void SubscribeToEvents()
        {
            _collisionDetector.OnPlayerCollision += OnCollisionWithEnemy;
        }

        public void UnsubscribeFromEvents()
        {
            _collisionDetector.OnPlayerCollision -= OnCollisionWithEnemy;
        }

        private void OnCollisionWithEnemy(EnemyBehaviour enemy)
        {
            if (!_transform || !enemy)
                return;

            float effectiveSpeed = Mathf.Max(_playerMovement.CurrentSpeed, 1f);

            Vector2 direction = (_transform.position - enemy.transform.position).normalized;

            float pushMagnitude = effectiveSpeed * _config.PushSpeed;

            pushMagnitude = Mathf.Clamp(pushMagnitude, 1f, _config.MovementSpeed);

            _playerMovement.AddImpulse(direction * pushMagnitude);
            enemy.AddImpulse(-direction * pushMagnitude);
        }
    }
}