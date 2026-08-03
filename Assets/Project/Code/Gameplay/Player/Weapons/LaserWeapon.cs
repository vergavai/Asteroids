using Project.Code.Common;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using Project.Code.Gameplay.Player.Projectiles.Lasers;

namespace Project.Code.Gameplay.Player.Weapons
{
    public class LaserWeapon : Weapon<LaserBehaviour>
    {
        private readonly LaserChargeController _chargeController;

        public LaserWeapon(IInputProvider input, ObjectPool<LaserBehaviour> laserPool,
            LaserChargeController chargeController, PlayerInvincibility invincibility)
            : base(input, laserPool, invincibility)
        {
            _chargeController = chargeController;
        }

        public override void SubscribeToEvents()
        {
            _input.LaserPerformed += ShootLaser;
        }

        public override void UnsubscribeFromEvents()
        {
            _input.LaserPerformed -= ShootLaser;
        }

        private void ShootLaser()
        {
            if (!_chargeController.TryConsumeCharge(out int slotIndex))
                return;

            if (!TrySpawnProjectile(out LaserBehaviour laser))
            {
                _chargeController.ReturnCharge(slotIndex);
                return;
            }
            
            laser.Shoot();
        }
    }
}