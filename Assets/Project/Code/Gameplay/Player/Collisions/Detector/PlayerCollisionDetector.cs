using System;
using Cysharp.Threading.Tasks;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Collisions.Invincibility;

namespace Project.Code.Gameplay.Player.Collisions.Detector
{
    public class PlayerCollisionDetector
    {
        private readonly PlayerInvincibility _invincibility;
        private readonly PlayerConfig _config;
        
        public event Action<EnemyBehaviour> OnPlayerCollision;
        public event Action OnCollision;

        public PlayerCollisionDetector(PlayerInvincibility invincibility, PlayerConfig config)
        {
            _invincibility = invincibility;
            _config = config;
        }

        public void TryInvokeCollisionAction(EnemyBehaviour enemy)
        {
            if (_invincibility.IsInvincible) return;
            
            OnPlayerCollision?.Invoke(enemy);
            OnCollision?.Invoke();
            
            _invincibility.ActivateInvincibility(_config.InvincibilityDuration).Forget();
        }
    }
}