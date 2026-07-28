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
        
        public AsteroidCreator(EnemyContainer container, AsteroidFactory factory, 
            EnemyConfig config, EnemyPrefabs prefabs)
            : base(container, factory)
        {
            _config = config;
            _prefabs = prefabs;
        }

        public override List<AsteroidBehaviour> Create()
        {
            return CreateObjects(_prefabs.AsteroidPrefab, _config.AsteroidCount, _container);
        }
    }
}