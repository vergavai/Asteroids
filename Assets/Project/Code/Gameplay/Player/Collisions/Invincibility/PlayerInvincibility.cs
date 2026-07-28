using System;
using Cysharp.Threading.Tasks;

namespace Project.Code.Gameplay.Player.Collisions.Invincibility
{
    public class PlayerInvincibility
    {
        private bool _isInvincible;

        public bool IsInvincible => _isInvincible;

        public event Action InvincibilityStarted;
        public event Action InvincibilityEnded;

        public async UniTaskVoid ActivateInvincibility(float duration)
        {
            _isInvincible = true;
            InvincibilityStarted?.Invoke();
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            _isInvincible = false;
            InvincibilityEnded?.Invoke();
        }
    }
}