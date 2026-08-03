using UnityEngine;

namespace Project.Code.Common
{
    public abstract class TransformHolder 
    {
        private Transform _transform;
        
        public Transform Transform => _transform;
        
        public TransformHolder(Transform transform)
        {
            _transform = transform;
        }
    }
}