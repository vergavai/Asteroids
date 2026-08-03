using System.Collections.Generic;
using Project.Code.Common;
using Project.Code.Configs;

namespace Project.Code.Gameplay.Player.Projectiles.Lasers.LaserGenerator
{
    public class LaserCreator : Creator<LaserBehaviour>
    {
        private ProjectilePrefabs _prefabs;
        private PlayerConfig _config;
        
        public LaserCreator(LasersTransformHolder transformHolder, LaserFactory factory, 
            ProjectilePrefabs prefabs, PlayerConfig config)
            : base(transformHolder, factory)
        {
            _prefabs = prefabs;
            _config = config;
        }

        public override List<LaserBehaviour> Create()
        {
            return CreateObjects(_prefabs.LaserPrefab, _config.LaserCount, _parent);
        }
    }
}