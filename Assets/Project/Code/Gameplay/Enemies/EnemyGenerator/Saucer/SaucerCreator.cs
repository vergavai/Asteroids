using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies.Saucer;

namespace Project.Code.Gameplay.Enemies.EnemyGenerator.Saucer
{
    public class SaucerCreator : Creator<SaucerBehaviour>
    {
        private EnemyConfig _config;
        private EnemyPrefabs _prefabs;
        
        public SaucerCreator(EnemyConfig config, EnemyPrefabs prefabs, 
            EnemyContainer container, SaucerFactory factory) : base(container, factory)
        {
            _config = config;
            _prefabs = prefabs;
        }

        public override List<SaucerBehaviour> Create()
        {
            return CreateObjects(_prefabs.SaucerPrefab, _config.SaucerCount, _container);
        }
    }
}