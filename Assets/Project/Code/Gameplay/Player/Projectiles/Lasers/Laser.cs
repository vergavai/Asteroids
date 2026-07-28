using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Rewards;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Projectiles.Lasers
{
    public class Laser
    {
        private GameObject _self;
        private Transform _transform;
        private Transform _player;
        private Vector3 _direction;
        private CancellationTokenSource _disableTokenSource;
        private PlayerConfig _config;
        private float _duration;
        private PlayerRewards _rewards;

        public Laser(PlayerConfig config, PlayerRewards rewards)
        {
            _config = config;
            _duration = _config.LaserDuration;
            _rewards = rewards;
        }

        public void Initialize(GameObject self, Transform player)
        {
            _self = self;
            _player = player;
            _transform = _self.transform;
            
            SetLaserSize();
        }

        public void OnHit(EnemyBehaviour enemy)
        {
            enemy.Kill();
            CancelAutoDisable();
            _disableTokenSource = new CancellationTokenSource();
            DisableAsync(_duration, _disableTokenSource.Token).Forget();
            _rewards.Reward(enemy.Type);
        }

        public void OnLaserShoot()
        {
            SetDirection();
            CancelAutoDisable();
            _disableTokenSource = new CancellationTokenSource();
            _self.SetActive(true);
            DisableAsync(_duration, _disableTokenSource.Token).Forget();
        }

        private void CancelAutoDisable()
        {
            _disableTokenSource?.Cancel();
            _disableTokenSource?.Dispose();
            _disableTokenSource = null;
        }

        private void SetDirection()
        {
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0, _player.transform.rotation.eulerAngles.z));
        }

        private async UniTaskVoid DisableAsync(float time, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            _self.SetActive(false);
        }

        private void SetLaserSize()
        {
            _transform.localScale = new Vector3(_config.LaserLength, _config.LaserHeight, _transform.localScale.z);
        }
    }
}