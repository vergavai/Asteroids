using UnityEngine;
using Zenject;
using Project.Code.Gameplay.Player.InputReading;
using Project.Code.Gameplay.Player.Rotating;
using Project.Code.Configs;
using Project.Code.Common;
using Project.Code.Gameplay.Player.Projectiles.Bullets;
using Project.Code.Gameplay.Player.Projectiles.Lasers;

namespace Project.Code.Gameplay.Player.Weapons
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private IInputProvider _inputProvider;
        private PlayerRotation _playerRotation;
        private PlayerConfig _config;
        private ObjectPool<BulletBehaviour> _bulletPool;
        private ObjectPool<LaserBehaviour> _laserPool;

        private BulletWeapon _bulletWeapon;
        private LaserWeapon _laserWeapon;
        private LaserChargeController _chargeController;

        [Inject]
        private void Construct(BulletWeapon bulletWeapon, LaserWeapon laserWeapon, LaserChargeController chargeController)
        {
            _bulletWeapon = bulletWeapon;
            _laserWeapon = laserWeapon;
            _chargeController = chargeController;

            _bulletWeapon.Initialize(_shootingPoint);
            _laserWeapon.Initialize(_shootingPoint);
        }

        private void OnEnable()
        {
            _bulletWeapon.SubscribeToEvents();
            _laserWeapon.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _bulletWeapon.UnsubscribeFromEvents();
            _laserWeapon.UnsubscribeFromEvents();
        }

        private void OnDestroy()
        {
            _chargeController?.Dispose();
        }
    }
}