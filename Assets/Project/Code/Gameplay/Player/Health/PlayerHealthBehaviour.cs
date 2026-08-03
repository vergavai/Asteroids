using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Health
{
    public class PlayerHealthBehaviour : MonoBehaviour
    {
        private PlayerHealth _health;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(PlayerHealth health, SignalBus signalBus)
        {
            _health = health;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(DisablePlayer);
            _health.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(DisablePlayer);
            _health.UnsubscribeFromEvents();
        }

        private void DisablePlayer()
        {
            gameObject.SetActive(false);
        }
    }
}