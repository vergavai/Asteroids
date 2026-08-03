using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Player.Projectiles.Bullets;
using Project.Code.Gameplay.Player.Projectiles.Bullets.BulletGenerator;
using Project.Code.Gameplay.Player.Projectiles.Lasers;
using Project.Code.Gameplay.Player.Projectiles.Lasers.LaserGenerator;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class ProjectilesInstaller : MonoInstaller
    {
        [SerializeField] private Transform _projectilesContainer;
        [SerializeField] private ProjectilePrefabs _projectilePrefabs;

        public override void InstallBindings()
        {
            BindProjectilesConfig();
            BindProjectilesContainer();
            BindProjectilePools();
            BindProjectileFactories();
            BindProjectileCreators();
            BindProjectiles();
        }

        private void BindProjectilePools()
        {
            Container.Bind<ObjectPool<BulletBehaviour>>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<ObjectPool<LaserBehaviour>>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindProjectilesContainer()
        {
            Container.Bind<BulletsTransformHolder>()
                .FromInstance(new BulletsTransformHolder(_projectilesContainer))
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LasersTransformHolder>()
                .FromInstance(new LasersTransformHolder(_projectilesContainer))
                .AsSingle()
                .NonLazy();
        }
        
        private void BindProjectileFactories()
        {
            Container.BindFactory<GameObject, BulletBehaviour, BulletFactory>()
                .FromFactory<PrefabFactory<BulletBehaviour>>();
            
            Container.BindFactory<GameObject, LaserBehaviour, LaserFactory>()
                .FromFactory<PrefabFactory<LaserBehaviour>>();
        }

        private void BindProjectilesConfig()
        {
            Container.Bind<ProjectilePrefabs>()
                .FromInstance(_projectilePrefabs)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindProjectileCreators()
        {
            Container.Bind<BulletCreator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LaserCreator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindProjectiles()
        {
            Container
                .Bind<Bullet>()
                .FromNew()
                .AsTransient();
            
            Container.Bind<Laser>()
                .FromNew()
                .AsTransient();
        }
    }
}