using System.Collections.Generic;
using Project.Code.Common;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Projectiles.Lasers.LaserGenerator
{
    public class LaserCreatorBehaviour : MonoBehaviour
    {
        private LaserCreator _laserCreator;
        private ObjectPool<LaserBehaviour> _pool;
        
        [Inject]
        private void Construct(LaserCreator laserCreator, ObjectPool<LaserBehaviour> pool)
        {
            _laserCreator = laserCreator;
            _pool = pool;
        }
        
        private void Start()
        {
            List<LaserBehaviour> lasers = _laserCreator.Create();
            _pool.AddObjects(lasers);
        }
    }
}