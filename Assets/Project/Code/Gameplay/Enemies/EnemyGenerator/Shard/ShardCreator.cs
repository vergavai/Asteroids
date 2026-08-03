using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies.Asteroid;

namespace Project.Code.Gameplay.Enemies.EnemyGenerator.Shard
{
    public class ShardCreator : Creator<AsteroidShardBehaviour>
    {
        private EnemyConfig _config;
        private EnemyPrefabs _prefabs;
        
        public ShardCreator(EnemyTransformHolder transformHolder, ShardFactory factory, EnemyConfig config, EnemyPrefabs prefabs) : 
            base(transformHolder, factory)
        {
            _config = config;
            _prefabs = prefabs;
        }

        public override List<AsteroidShardBehaviour> Create()
        {
            return CreateObjects(_prefabs.ShardPrefab, _config.AsteroidCount * _config.ShardsPerAsteroid, _parent);
        }
    }
}