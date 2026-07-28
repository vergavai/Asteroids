using Project.Code.Gameplay.Enemies.Type;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        public EnemyType Type;
        
        public virtual void AddImpulse(Vector2 impulse) { }

        public abstract void Kill();
    }
}