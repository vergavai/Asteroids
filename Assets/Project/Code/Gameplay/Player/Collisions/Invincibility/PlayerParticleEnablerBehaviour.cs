using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Collisions.Invincibility
{
    public class PlayerParticleEnablerBehaviour : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        private PlayerParticleEnabler _particleEnabler;
        
        [Inject]
        private void Construct(PlayerParticleEnabler particleEnabler)
        {
            _particleEnabler = particleEnabler;
            _particleEnabler.Initialize(_particleSystem);
        }

        private void OnEnable()
        {
            _particleEnabler.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _particleEnabler.UnsubscribeFromEvents();
        }
    }
}