using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Player.InputReading;
using Project.Code.Gameplay.Player.Projectiles.Bullets;
using Project.Code.Gameplay.Player.Projectiles.Lasers;
using Project.Code.Gameplay.Player.Rotating;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Weapons
{
    public class PlayerGun
    {
        private PlayerInput _input;
        private ObjectPool<BulletBehaviour> _bulletPool;
        private ObjectPool<LaserBehaviour> _laserPool;
        private Transform _gunPoint;
        private Transform _laserPoint;
        private PlayerConfig _config;
        private PlayerRotation _rotation;

        private int _maxCharges;
        private int _currentCharges;
        private float _currentCooldown;
        private float[] _chargeTimers;
        private CancellationTokenSource _rechargeTokenSource;
        private bool _isRegenerating;

        public int CurrentCharges => _currentCharges;
        public float[] ChargeTimers => _chargeTimers;

        public PlayerGun(PlayerInput input, ObjectPool<BulletBehaviour> bulletPool,
            ObjectPool<LaserBehaviour> laserPool, PlayerConfig config, PlayerRotation rotation)
        {
            _input = input;
            _rotation = rotation;
            _bulletPool = bulletPool;
            _laserPool = laserPool;
            _config = config;
            _maxCharges = _config.LaserMaxCharges;
            _currentCooldown = _config.LaserCooldown;
            _currentCharges = _maxCharges;
            _chargeTimers = new float[_maxCharges];
            
            for (int i = 0; i < _maxCharges; i++)
                _chargeTimers[i] = 0f;
            
            _rechargeTokenSource = new CancellationTokenSource();
        }

        public void Initialize(Transform gunPoint, Transform laserPoint)
        {
            _gunPoint = gunPoint;
            _laserPoint = laserPoint;
        }

        public void SubscribeToEvents()
        {
            _input.ShootRequested += ShootBullet;
            _input.LaserRequested += ShootLaser;
            _rechargeTokenSource?.Dispose();
            _rechargeTokenSource = new CancellationTokenSource();
        }

        public void UnsubscribeFromEvents()
        {
            _input.ShootRequested -= ShootBullet;
            _input.LaserRequested -= ShootLaser;
            _rechargeTokenSource?.Cancel();
            _rechargeTokenSource?.Dispose();
        }

        private void ShootBullet()
        {
            if (!_bulletPool.TryGetRandomObject(out BulletBehaviour bullet))
                return;

            bullet.transform.position = _gunPoint.position;
            bullet.SetDirection(_rotation.Direction);
            bullet.gameObject.SetActive(true);
        }

        private void ShootLaser()
        {
            if (!TryConsumeLaserCharge(out int slotIndex))
                return;

            if (!_laserPool.TryGetRandomObject(out LaserBehaviour laser))
            {
                _currentCharges++;
                _chargeTimers[slotIndex] = 0f;
                return;
            }

            laser.transform.position = _laserPoint.position;
            laser.Shoot();

            StartRegenerationIfNeeded();
        }
        
        private bool TryConsumeLaserCharge(out int slotIndex)
        {
            slotIndex = -1;
            if (_currentCharges <= 0)
                return false;

            for (int i = 0; i < _chargeTimers.Length; i++)
            {
                if (Mathf.Approximately(_chargeTimers[i], 0f))
                {
                    slotIndex = i;
                    break;
                }
            }

            if (slotIndex == -1)
                return false;

            _currentCharges--;
            _chargeTimers[slotIndex] = _currentCooldown;
            return true;
        }
        
        private void StartRegenerationIfNeeded()
        {
            if (!_isRegenerating && _rechargeTokenSource is { IsCancellationRequested: false })
            {
                _isRegenerating = true;
                RegenerateChargesAsync(_rechargeTokenSource.Token).Forget();
            }
        }

        private async UniTaskVoid RegenerateChargesAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    bool anyActive = false;
                    for (int i = 0; i < _chargeTimers.Length; i++)
                    {
                        if (_chargeTimers[i] > 0f)
                        {
                            _chargeTimers[i] -= Time.deltaTime;
                            if (_chargeTimers[i] <= 0f)
                            {
                                _chargeTimers[i] = 0f;
                                _currentCharges++;
                            }
                            anyActive = true;
                        }
                    }

                    if (!anyActive)
                        break;

                    await UniTask.Yield(PlayerLoopTiming.Update);
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                _isRegenerating = false;
            }
        }
        
    }
}