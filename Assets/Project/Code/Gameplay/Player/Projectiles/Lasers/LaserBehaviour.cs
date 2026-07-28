using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Player.Movement;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Projectiles.Lasers
{
    public class LaserBehaviour : MonoBehaviour
    {
        private Laser _laser;

        [Inject]
        private void Construct(Laser laser, PlayerMovementBehaviour player)
        {
            _laser = laser;
            _laser.Initialize(gameObject, player.transform);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyBehaviour enemy))
            {
                _laser.OnHit(enemy);
            }
        }

        public void Shoot()
        {
            _laser.OnLaserShoot();
        }
    }
}