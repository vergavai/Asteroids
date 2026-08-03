using Project.Code.Gameplay.Player.Movement;
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
            _enemy = _saucer;
        }

        public override void Kill()
        {
            _enemy.ResetState();
            gameObject.SetActive(false);
        }
    }
}