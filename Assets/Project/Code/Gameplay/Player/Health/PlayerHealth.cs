using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Collisions.Detector;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Health
{
    public class PlayerHealth
    {
        private PlayerCollisionDetector _collisionDetector;
        private PlayerConfig _config;
        private readonly SignalBus _signalBus;
        
        private int _currentHealth;

        public PlayerHealth(PlayerConfig config, PlayerCollisionDetector collisionDetector, SignalBus signalBus)
        {
            _collisionDetector = collisionDetector;
            _config = config;
            _signalBus = signalBus;

            _currentHealth = _config.Hearts;
        }

        public void SubscribeToEvents()
        {
            _collisionDetector.OnCollision += OnCollisionWithEnemy;
        }

        public void UnsubscribeFromEvents()
        {
            _collisionDetector.OnCollision -= OnCollisionWithEnemy;
        }

        private void OnCollisionWithEnemy()
        {
            _currentHealth = Mathf.Max(_currentHealth - 1, 0);

            if (_currentHealth <= 0)
            {
                _signalBus.Fire<PlayerDiedSignal>();
            }
        }
    }
}