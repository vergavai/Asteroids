using Project.Code.Configs;
using Project.Code.Gameplay.Player.Collisions.Detector;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.Collisions.Pusher;
using Project.Code.Gameplay.Player.Health;
using Project.Code.Gameplay.Player.Info;
using Project.Code.Gameplay.Player.InputReading;
using Project.Code.Gameplay.Player.Movement;
using Project.Code.Gameplay.Player.Rewards;
using Project.Code.Gameplay.Player.Rotating;
using Project.Code.Gameplay.Player.Weapons;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller    
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _spawnPoint;

        public override void InstallBindings()
        {
            BindSignals();
            BindConfig();
            BindPlayerRewards();
            BindPlayerCollisions();
            BindPlayerHealth();
            BindPlayerInput();
            BindWeapons();
            BindPlayerRotation();
            BindPlayerMovement();
            BindPlayerInfo();
            BindAndInstantiatePlayer();
        }
        
        private void BindConfig()
        {
            PlayerConfig config = ConfigLoader.LoadConfig<PlayerConfig>("Configs/PlayerConfig");
            if (config != null)
            {
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
        
        private void BindWeapons()
        {
            Container.Bind<BulletWeapon>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LaserChargeController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LaserWeapon>()
                .FromNew()
                .AsSingle()
                .NonLazy();
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