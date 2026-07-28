using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Rewards;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Projectiles.Bullets
{
    public class Bullet
    {
        private Transform _transform;
        private Camera _camera;
        private Vector3 _direction;
        private GameObject _self;
        private PlayerConfig _config;
        private float _speed;
        private PlayerRewards _rewards;

        public Bullet(Camera camera, PlayerConfig config, PlayerRewards rewards)
        {
            _camera = camera;
            _config = config;
            _speed = _config.BulletSpeed;
            _rewards = rewards;
        }

        public void Initialize(Transform transform, GameObject self)
        {
            _transform = transform; 
            _self = self;
        }

        public void UpdatePosition()
        {
            _transform.Translate(_direction * (_speed * Time.deltaTime), Space.World);
        }

        public void SetDirection()
        {
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            _direction = (mouseWorldPos - _transform.position).normalized;
        }

        public void OnHit(EnemyBehaviour enemy)
        {
            enemy.Kill();
            _self.SetActive(false);
            _rewards.Reward(enemy.Type);
        }
    }
}