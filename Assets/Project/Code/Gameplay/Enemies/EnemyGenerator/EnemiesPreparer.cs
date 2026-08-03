using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies.Asteroid;
using Project.Code.Gameplay.Enemies.EnemyGenerator.Asteroid;
using Project.Code.Gameplay.Enemies.EnemyGenerator.Saucer;
using Project.Code.Gameplay.Enemies.EnemyGenerator.Shard;
using Project.Code.Gameplay.Enemies.Saucer;
using UnityEngine;

namespace Project.Code.Gameplay.Enemies.EnemyGenerator
{
    public class EnemiesPreparer
    {
        private EnemyConfig _config;
        private ShardCreator _shardCreator;
        private AsteroidCreator _asteroidCreator;
        private SaucerCreator _saucerCreator;
        private ObjectPool<EnemyBehaviour> _enemyPool;
        private ObjectPool<AsteroidShardBehaviour> _shardPool;
        
        public EnemiesPreparer(ObjectPool<EnemyBehaviour> enemyPool, 
            ObjectPool<AsteroidShardBehaviour> shardPool, EnemyConfig config,
            ShardCreator shardCreator, AsteroidCreator asteroidCreator, 
            SaucerCreator saucerCreator) 
        {
            _config = config;
            _shardCreator = shardCreator;
            _asteroidCreator = asteroidCreator;
            _enemyPool = enemyPool;
            _shardPool = shardPool;
            _saucerCreator = saucerCreator;
        }
        
        public void CreateAndAddObjects()
        {
            List<AsteroidShardBehaviour> shards = _shardCreator.Create();
            List<AsteroidBehaviour> asteroids = _asteroidCreator.Create();
            List<SaucerBehaviour> saucers = _saucerCreator.Create();
            
            _shardPool.AddObjects(shards);
            _enemyPool.AddObjects(asteroids);
            _enemyPool.AddObjects(saucers);
            
            FillAsteroidsWithShards(asteroids, shards);
        }
        
        private void FillAsteroidsWithShards(List<AsteroidBehaviour> asteroids, List<AsteroidShardBehaviour> shards)
        {
            int expectedTotal = asteroids.Count * _config.ShardsPerAsteroid;

            if (shards.Count != expectedTotal)
            {
                Debug.LogError("Incorrect number of shards");
                return;
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                int startIndex = i * _config.ShardsPerAsteroid;
                AsteroidShardBehaviour[] shardGroup = new AsteroidShardBehaviour[_config.ShardsPerAsteroid];
                for (int j = 0; j < _config.ShardsPerAsteroid; j++)
                {
                    shardGroup[j] = shards[startIndex + j];
                }
                asteroids[i].SetShards(shardGroup);
            }
        }
    }
}
