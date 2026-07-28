using System;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Collisions.Invincibility;

namespace Project.Code.Gameplay.Player.Collisions.Detector
{
    public class PlayerCollisionDetector
    {
        private readonly PlayerInvincibility _invincibility;
        
        public event Action<EnemyBehaviour> OnPlayerCollision;

        public PlayerCollisionDetector(PlayerInvincibility invincibility)
        {
            _invincibility = invincibility;
        }

        public void TryInvokeCollisionAction(EnemyBehaviour enemy)
        {
            if (_invincibility.IsInvincible) return;
            
            OnPlayerCollision?.Invoke(enemy);
            
            _invincibility.ActivateInvincibility(3f).Forget();
        }
    }
}