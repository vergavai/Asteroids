using System.Collections.Generic;
using UnityEngine;

namespace Project.Code.Common
{
    public class ObjectPool<T> where T : Component
    {
        private List<T> _objects = new();
        private Camera _camera;

        public ObjectPool(Camera camera)
        {
            _camera = camera;
        }
        
        public bool TryGetRandomObject(out T result)
        {
            result = null;
            int disabledCount = 0;
            T selected = null;

            for (int i = 0; i < _objects.Count; i++)
            {
                if (!_objects[i].gameObject.activeSelf)
                {
                    disabledCount++;
                    if (Random.Range(0, disabledCount) == 0)
                    {
                        selected = _objects[i];
                    }
                }
            }

            result = selected;
            return result;
        }

        public void AddObjects(IEnumerable<T> objects)
        {
            foreach (var obj in objects) _objects.Add(obj);
        }

        public void DisableObjectsOutsideCamera(float padding = 0f)
        {
            Vector3 bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
            Vector3 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));

            float minX = bottomLeft.x - padding;
            float maxX = topRight.x + padding;
            float minY = bottomLeft.y - padding;
            float maxY = topRight.y + padding;

            for (int i = 0; i < _objects.Count; i++)
            {
                T item = _objects[i];
                if (!item.gameObject.activeSelf) continue;

                Vector3 pos = item.transform.position;
                bool isInside = pos.x >= minX && pos.x <= maxX && pos.y >= minY && pos.y <= maxY;

                if (!isInside)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }

    }
}