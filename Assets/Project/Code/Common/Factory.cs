using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Code.Common
{
    public abstract class Factory<T> : PlaceholderFactory<GameObject, T> where T : Component
    {
        public List<T> CreateMany(GameObject prefab, int count, Transform parent)
        {
            List<T> objects = new List<T>();

            for (int i = 0; i < count; i++)
            {
                T obj = Create(prefab);
                obj.gameObject.SetActive(false);
                
                objects.Add(obj);
                obj.transform.SetParent(parent);
            }

            return objects;
        }
    }
}