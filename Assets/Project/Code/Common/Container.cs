using UnityEngine;

namespace Project.Code.Common
{
    public abstract class Container 
    {
        private Transform _transform;
        
        public Transform Transform => _transform;
        
        public Container(Transform transform)
        {
            _transform = transform;
        }
    }
}