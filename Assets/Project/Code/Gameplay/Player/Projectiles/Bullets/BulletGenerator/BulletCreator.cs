using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Projectiles.Bullets.BulletGenerator
{
    public class BulletCreator : Creator<BulletBehaviour>
    {
        private ProjectilePrefabs _prefabs;
        private PlayerConfig _config;
        
        public BulletCreator(BulletsContainer container, BulletFactory factory, ProjectilePrefabs prefabs, PlayerConfig config) 
            : base(container, factory)
        {
            _prefabs = prefabs;
            _config = config;
        }

        public override List<BulletBehaviour> Create()
        {
            return CreateObjects(_prefabs.BulletPrefab, _config.BulletCount, _container);
        }
    }
}