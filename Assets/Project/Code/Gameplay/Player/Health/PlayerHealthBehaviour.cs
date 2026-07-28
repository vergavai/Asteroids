using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Health
{
    public class PlayerHealthBehaviour : MonoBehaviour
    {
        private PlayerHealth _health;

        [Inject]
        private void Construct(PlayerHealth health)
        {
            _health = health;
            
            _health.Initialize(gameObject);
        }

        private void OnEnable()
        {
            _health.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _health.UnsubscribeFromEvents();
        }
    }
}