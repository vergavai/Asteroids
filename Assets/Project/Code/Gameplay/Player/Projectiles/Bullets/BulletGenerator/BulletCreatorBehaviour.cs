using System.Collections.Generic;
using Project.Code.Common;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Projectiles.Bullets.BulletGenerator
{
    public class BulletCreatorBehaviour : MonoBehaviour
    {
        private BulletCreator _bulletCreator;
        private ObjectPool<BulletBehaviour> _pool;

        [Inject]
        private void Construct(BulletCreator bulletCreator, ObjectPool<BulletBehaviour> pool)
        {
            _bulletCreator = bulletCreator;
            _pool = pool;
        }

        private void Update()
        {
            _pool.DisableObjectsOutsideCamera();
        }
        
        private void Start()
        {
            List<BulletBehaviour> bullets = _bulletCreator.Create();
            _pool.AddObjects(bullets);
        }
    }
}