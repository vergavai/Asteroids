using UnityEngine;

namespace Project.Code.Gameplay.Player.Collisions.Invincibility
{
    public class PlayerParticleEnabler
    {
        private ParticleSystem _particleSystem;
        private PlayerInvincibility _invincibility;

        public PlayerParticleEnabler(PlayerInvincibility invincibility)
        {
            _invincibility = invincibility;
        }

        public void Initialize(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public void SubscribeToEvents()
        {
            _invincibility.InvincibilityStarted += StartParticleSystem;
            _invincibility.InvincibilityEnded += StopParticleSystem;
        }

        public void UnsubscribeFromEvents()
        {
            _invincibility.InvincibilityStarted -= StartParticleSystem;
            _invincibility.InvincibilityEnded -= StopParticleSystem;
        }

        private void StartParticleSystem()
        {
            _particleSystem.Play();
        }

        private void StopParticleSystem()
        {
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}