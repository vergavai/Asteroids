using Project.Code.Configs;
using Project.Code.Gameplay.GameBounds;
using Project.Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            BindCamera();
            BindAnalytics();
            BindGameConfig();
            BindGameBounds();
        }
        
        private void BindCamera()
        {
            Container.Bind<Camera>()
                .FromInstance(_camera)
                .AsSingle()
                .NonLazy();
        }

        private void BindAnalytics()
        {
            Container.Bind<Analytics.AnalyticsService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameConfig()
        {
            GameConfig config = ConfigLoader.LoadConfig<GameConfig>("Configs/GameConfig");
            if (config != null)
            {
                Container.Bind<GameConfig>()
                    .FromInstance(config)
                    .AsSingle()
                    .NonLazy();
            }
        }

        private void BindGameBounds()
        {
            Container.Bind<GameBounds>().AsSingle().NonLazy();
        }
    }
}