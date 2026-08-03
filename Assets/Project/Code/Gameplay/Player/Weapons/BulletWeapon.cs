using Project.Code.Common;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using Project.Code.Gameplay.Player.Projectiles.Bullets;
using Project.Code.Gameplay.Player.Rotating;

namespace Project.Code.Gameplay.Player.Weapons
{
    public class BulletWeapon : Weapon<BulletBehaviour>
    {
        private readonly PlayerRotation _rotation;

        public BulletWeapon(IInputProvider input, ObjectPool<BulletBehaviour> bulletPool, 
            PlayerRotation rotation, PlayerInvincibility invincibility)
            : base(input, bulletPool, invincibility)
        {
            _rotation = rotation;
        }

        public override void SubscribeToEvents()
        {
            _input.ShootPerformed += ShootBullet;
        }

        public override void UnsubscribeFromEvents()
        {
            _input.ShootPerformed -= ShootBullet;
        }

        private void ShootBullet()
        {
            if (TrySpawnProjectile(out BulletBehaviour bullet))
            {
                bullet.SetDirection(_rotation.Direction);
            }
        }
    }
}