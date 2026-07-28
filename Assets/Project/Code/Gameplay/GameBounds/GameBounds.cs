using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.GameBounds
{
    public class GameBounds
    {
        private Transform _transform;
        private Camera _camera;

        private float _leftBound;
        private float _rightBound;
        private float _topBound;
        private float _bottomBound;

        public GameBounds(Camera camera, GameConfig config)
        {
            _camera = camera;

            _camera.orthographicSize = config.MapSize;
            
            _leftBound = camera.ViewportToWorldPoint(new Vector3(0, 0.5f, -camera.transform.position.z)).x;
            _topBound = camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, -camera.transform.position.z)).y;
            _rightBound = -_leftBound;
            _bottomBound = -_topBound;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void UpdateBounds()
        {
            Vector3 pos = _transform.position;

            if (pos.x < _leftBound)
            {
                pos.x = _rightBound;
            }
            else if (pos.x > _rightBound)
            {
                pos.x = _leftBound;
            }
            
            if (pos.y > _topBound)
            {
                pos.y = _bottomBound;
            }
            else if (pos.y < _bottomBound)
            {
                pos.y = _topBound;
            }

            _transform.position = pos;
        }
    }
}