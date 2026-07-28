using System.IO;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies;
using Project.Code.Gameplay.Enemies.Asteroid;
using Project.Code.Gameplay.Enemies.EnemyGenerator;
using Project.Code.Gameplay.Enemies.EnemyGenerator.Asteroid;
using Project.Code.Gameplay.Enemies.EnemyGenerator.Saucer;
using Project.Code.Gameplay.Enemies.EnemyGenerator.Shard;
using Project.Code.Gameplay.Enemies.Saucer;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemyPrefabs enemyPrefabs;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindEnemies();
            BindEnemyPool();
            BindFactories();
            BindEnemyContainer();
            BindEnemyCreator();
        }

        private void BindFactories()
        {
            Container.BindFactory<GameObject, AsteroidShardBehaviour, ShardFactory>()
                .FromFactory<PrefabFactory<AsteroidShardBehaviour>>();
            
            Container.BindFactory<GameObject, AsteroidBehaviour, AsteroidFactory>()
                .FromFactory<PrefabFactory<AsteroidBehaviour>>();
            
            Container.BindFactory<GameObject, SaucerBehaviour, SaucerFactory>()
                .FromFactory<PrefabFactory<SaucerBehaviour>>();
        }

        private void BindEnemyContainer()
        {
            Container.Bind<EnemyContainer>()
                .FromInstance(new EnemyContainer(_enemyContainer))
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyPool()
        {
            Container.Bind<ObjectPool<EnemyBehaviour>>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<ObjectPool<AsteroidShardBehaviour>>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyCreator()
        {
            Container.Bind<AsteroidCreator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<ShardCreator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<SaucerCreator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<EnemiesPreparer>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemies()
        {
            Container.Bind<Asteroid>()
                .FromNew()
                .AsTransient()
                .NonLazy();

            Container.Bind<AsteroidShard>()
                .FromNew()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<Saucer>()
                .FromNew()
                .AsTransient()
                .NonLazy();
        }

        private void BindConfigs()
        {
            EnemyConfig config;
            
            string filePath = "Assets/Project/Resources/Configs/EnemyConfig.json";
        
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                config = JsonConvert.DeserializeObject<EnemyConfig>(json);
                Container.Bind<EnemyConfig>()
                    .FromInstance(config)
                    .AsSingle()
                    .NonLazy();
            }
            
            Container.Bind<EnemyPrefabs>()
                .FromInstance(enemyPrefabs)
                .AsSingle()
                .NonLazy();
        }
    }
}