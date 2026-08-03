using UnityEngine;

namespace Project.Code.Gameplay.Enemies
{
    public abstract class Enemy
    {
        protected const float Epsilon = 0.0001f;
        protected Transform _transform;
        protected Vector2 PushVelocity;

        public void AddImpulse(Vector2 impulse)
        {
            PushVelocity += impulse;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public virtual void UpdatePosition()
        {
            if (PushVelocity.sqrMagnitude > Epsilon)
            {
                PushVelocity = Vector2.Lerp(PushVelocity, Vector2.zero, Time.deltaTime);
                if (PushVelocity.sqrMagnitude < Epsilon)
                    PushVelocity = Vector2.zero;

                _transform.Translate(PushVelocity * Time.deltaTime, Space.World);
            }
        }

        public virtual void ResetState()
        {
            PushVelocity = Vector2.zero;
        }
    }
}