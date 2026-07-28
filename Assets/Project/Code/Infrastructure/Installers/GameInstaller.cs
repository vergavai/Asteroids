using System.IO;
using Project.Code.Configs;
using Project.Code.Gameplay.GameBounds;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
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
                .FromInstance(Camera.main)
                .AsSingle()
                .NonLazy();
        }

        private void BindAnalytics()
        {
            Container.Bind<Analytics.Analytics>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameConfig()
        {
            GameConfig config;

            string filePath = "Assets/Project/Resources/Configs/GameConfig.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                config = JsonConvert.DeserializeObject<GameConfig>(json);
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