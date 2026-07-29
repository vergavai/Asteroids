using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Rewards;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Projectiles.Bullets
{
    public class Bullet
    {
        private Transform _transform;
        private GameObject _self;
        private PlayerConfig _config;
        private float _speed;
        private PlayerRewards _rewards;
        private Vector3 _direction;

        public Bullet(PlayerConfig config, PlayerRewards rewards)
        {
            _config = config;
            _speed = _config.BulletSpeed;
            _rewards = rewards;
        }

        public void Initialize(Transform transform, GameObject self)
        {
            _transform = transform;
            _self = self;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        public void UpdatePosition()
        {
            _transform.Translate(_direction * (_speed * Time.deltaTime), Space.World);
        }

        public void OnHit(EnemyBehaviour enemy)
        {
            enemy.Kill();
            _self.SetActive(false);
            _rewards.Reward(enemy.Type);
        }
    }
}