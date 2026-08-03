using Project.Code.Gameplay.Enemies.Type;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyType _type;

        protected Enemy _enemy;

        public EnemyType Type => _type;

        public virtual void AddImpulse(Vector2 impulse)
        {
            _enemy.AddImpulse(impulse);
        }

        protected virtual void Update()
        {
            _enemy.UpdatePosition();
        }

        public abstract void Kill();
    }
}