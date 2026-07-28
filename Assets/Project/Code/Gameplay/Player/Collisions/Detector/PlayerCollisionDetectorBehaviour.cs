using Project.Code.Gameplay.Enemies;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Collisions.Detector
{
    public class PlayerCollisionDetectorBehaviour : MonoBehaviour
    {
        private PlayerCollisionDetector _playerCollisionDetector;

        [Inject]
        private void Construct(PlayerCollisionDetector playerCollisionDetector)
        {
            _playerCollisionDetector = playerCollisionDetector;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyBehaviour enemy))
            {
                _playerCollisionDetector.TryInvokeCollisionAction(enemy);
            }
        }
    }
}
