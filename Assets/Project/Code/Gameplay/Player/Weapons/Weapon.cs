using Project.Code.Common;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Weapons
{
    public abstract class Weapon<T> where T : Component
    {
        protected readonly IInputProvider _input;
        protected readonly ObjectPool<T> _pool;
        protected Transform _firePoint;

        private PlayerInvincibility _invincibility;

        protected Weapon(IInputProvider input, ObjectPool<T> pool, PlayerInvincibility invincibility)
        {
            _input = input;
            _pool = pool;
            _invincibility = invincibility;
        }

        public void Initialize(Transform firePoint)
        {
            _firePoint = firePoint;
        }

        public abstract void SubscribeToEvents();
        public abstract void UnsubscribeFromEvents();

        protected bool TrySpawnProjectile(out T projectile)
        {
            projectile = null;
            
            if (_invincibility.IsInvincible) return false;
            if (!_pool.TryGetRandomObject(out projectile)) return false;

            projectile.transform.position = _firePoint.position;
            projectile.gameObject.SetActive(true);
            
            return true;
        }
    }
}