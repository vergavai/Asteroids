using System.Collections.Generic;
using UnityEngine;

namespace Project.Code.Common
{
    public abstract class Creator<T> where T : Component
    {
        protected Factory<T> _factory;
        protected Transform _container;

        public Creator(Container container, Factory<T> factory)
        {
            _container = container.Transform;
            _factory = factory;
        }

        public abstract List<T> Create();

        protected List<T> CreateObjects(GameObject prefab, int count, Transform parent)
        {
            List<T> enemies = _factory.CreateMany(prefab, count, parent);

            return enemies;
        }
    }
}