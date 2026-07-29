using System.IO;
using Newtonsoft.Json;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Player.Collisions.Detector;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.Collisions.Pusher;
using Project.Code.Gameplay.Player.Health;
using Project.Code.Gameplay.Player.Info;
using Project.Code.Gameplay.Player.InputReading;
using Project.Code.Gameplay.Player.Movement;
using Project.Code.Gameplay.Player.Projectiles.Bullets;
using Project.Code.Gameplay.Player.Projectiles.Bullets.BulletGenerator;
using Project.Code.Gameplay.Player.Projectiles.Lasers;
using Project.Code.Gameplay.Player.Projectiles.Lasers.LaserGenerator;
using Project.Code.Gameplay.Player.Rewards;
using Project.Code.Gameplay.Player.Rotating;
using Project.Code.Gameplay.Player.Weapons;
using Project.Code.UI.Visibility;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private ForceMobileCheck _forceMobile;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private ProjectilePrefabs projectilePrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _projectilesContainer;

        public override void InstallBindings()
        {
            BindSignals();
            BindConfig();
            BindInputProvider();
            BindProjectilesConfig();
            BindProjectilesContainer();
            BindProjectilePools();
            BindProjectileFactories();
            BindProjectileCreators();
            BindPlayerRewards();
            BindProjectiles();
            BindPlayerCollisions();
            BindPlayerHealth();
            BindPlayerInput();
            BindPlayerGun();
            BindPlayerRotation();
            BindPlayerMovement();
            BindPlayerInfo();
            BindAndInstantiatePlayer();
        }
        
        private void BindInputProvider()
        {
            bool useMobile = _forceMobile.ForceMobileInput || SystemInfo.deviceType == DeviceType.Handheld;
            
            if (!useMobile)
            {
                Container.Bind<IInputProvider>()
                    .To<DesktopInputProvider>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<MobileInputProvider>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();
            }
        }

        private void BindConfig()
        {
            PlayerConfig config;

            string filePath = "Assets/Project/Resources/Configs/playerConfig.json";
        
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                config = JsonConvert.DeserializeObject<PlayerConfig>(json);
                Container.Bind<PlayerConfig>()
                    .FromInstance(config)
                    .AsSingle()
                    .NonLazy();
            }
        }
        
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayerDiedSignal>();
        }

        private void BindAndInstantiatePlayer()
        {
            Container.Bind<PlayerMovementBehaviour>()
                .FromComponentInNewPrefab(_playerPrefab)
                .AsSingle()
                .OnInstantiated<PlayerMovementBehaviour>(SetPlayerPosition)
                .NonLazy();
        }

        private void SetPlayerPosition(InjectContext context, PlayerMovementBehaviour player)
        {
            player.transform.position = _spawnPoint.position;
            
            IInputProvider inputProvider = Container.Resolve<IInputProvider>();
            
            if (inputProvider is DesktopInputProvider desktopProvider)
            {
                desktopProvider.Initialize(player.transform);
            }
        }

        private void BindPlayerMovement()
        {
            Container.Bind<PlayerMovement>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerInput()
        {
            Container.Bind<PlayerInput>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerRotation()
        {
            Container.Bind<PlayerRotation>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerCollisions()
        {
            Container.Bind<PlayerInvincibility>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerCollisionDetector>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerPusher>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            
            
            Container.Bind<PlayerParticleEnabler>()
                .FromNew()
                .AsSingle()
                .NonLazy();
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

        private void BindPlayerGun()
        {
            Container.Bind<PlayerGun>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindProjectilesContainer()
        {
            Container.Bind<BulletsContainer>()
                .FromInstance(new BulletsContainer(_projectilesContainer))
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LasersContainer>()
                .FromInstance(new LasersContainer(_projectilesContainer))
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
                .FromInstance(projectilePrefabs)
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

        private void BindPlayerInfo()
        {
            Container.BindInterfacesAndSelfTo<PlayerInfo>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerHealth()
        {
            Container.Bind<PlayerHealth>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerRewards()
        {
            Container.Bind<PlayerRewards>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}