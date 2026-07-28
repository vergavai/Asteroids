using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Gameplay.Player.Projectiles.Bullets;
using Project.Code.Gameplay.Player.Projectiles.Bullets.BulletGenerator;
using Project.Code.Gameplay.Player.Projectiles.Lasers;
using Project.Code.Gameplay.Player.Projectiles.Lasers.LaserGenerator;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Projectiles
{
    public class ProjectilesCreatorBehaviour : MonoBehaviour
    {
        private ObjectPool<LaserBehaviour> _laserPool;
        private ObjectPool<BulletBehaviour> _bulletPool;
        private BulletCreator _bulletCreator;
        private LaserCreator _laserCreator;
        
        [Inject]
        private void Construct(LaserCreator laserCreator, BulletCreator bulletCreator, 
            ObjectPool<LaserBehaviour> laserPool, ObjectPool<BulletBehaviour> bulletPool)
        {
            _laserCreator = laserCreator;
            _laserPool = laserPool;
            _bulletCreator = bulletCreator;
            _bulletPool = bulletPool;
        }
        
        private void Start()
        {
            List<LaserBehaviour> lasers = _laserCreator.Create();
            List<BulletBehaviour> bullets = _bulletCreator.Create();
            _laserPool.AddObjects(lasers);
            _bulletPool.AddObjects(bullets);
        }
        
        private void Update()
        {
            _bulletPool.DisableObjectsOutsideCamera();
        }
    }
}