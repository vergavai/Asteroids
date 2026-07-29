using Project.Code.Gameplay.Enemies;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Projectiles.Bullets
{
    public class BulletBehaviour : MonoBehaviour
    {
        private Bullet _bullet;

        [Inject]
        private void Construct(Bullet bullet)
        {
            _bullet = bullet;
            _bullet.Initialize(transform, gameObject);
        }

        public void SetDirection(Vector3 direction)
        {
            _bullet.SetDirection(direction);
        }

        private void Update()
        {
            _bullet.UpdatePosition();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EnemyBehaviour>(out var enemy))
            {
                _bullet.OnHit(enemy);
            }
        }
    }
}