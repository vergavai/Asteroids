using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies.Asteroid;

namespace Project.Code.Gameplay.Enemies.EnemyGenerator.Asteroid
{
    public class AsteroidCreator : Creator<AsteroidBehaviour>
    {
        private EnemyConfig _config;
        private EnemyPrefabs _prefabs;
        
        public AsteroidCreator(EnemyTransformHolder transformHolder, AsteroidFactory factory, 
            EnemyConfig config, EnemyPrefabs prefabs)
            : base(transformHolder, factory)
        {
            _config = config;
            _prefabs = prefabs;
        }

        public override List<AsteroidBehaviour> Create()
        {
            return CreateObjects(_prefabs.AsteroidPrefab, _config.AsteroidCount, _parent);
        }
    }
}