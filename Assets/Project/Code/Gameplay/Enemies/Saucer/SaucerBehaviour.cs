using Project.Code.Gameplay.Player.Movement;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Enemies.Saucer
{
    public class SaucerBehaviour : EnemyBehaviour
    {
        private Saucer _saucer;

        [Inject]
        private void Construct(Saucer saucer, PlayerMovementBehaviour player)
        {
            _saucer = saucer;
            _saucer.Initialize(transform, player.transform);
        }

        public override void AddImpulse(Vector2 impulse)
        {
            _saucer.AddImpulse(impulse);
        }

        private void Update()
        {
            _saucer.UpdatePosition();
        }

        public override void Kill()
        {
            _saucer.ResetState();
            gameObject.SetActive(false);
        }
    }
}