using Unity.Plastic.Newtonsoft.Json;

namespace Project.Code.Configs
{
    public class GameConfig
    {
        [JsonProperty("MapSize")] private float _mapSize;
        [JsonProperty("AsteroidReward")] private float _asteroidReward;
        [JsonProperty("ShardReward")] private float _shardReward;
        [JsonProperty("SaucerReward")] private float _saucerReward;

        [JsonIgnore] public float MapSize => _mapSize;
        [JsonIgnore] public float AsteroidReward => _asteroidReward;
        [JsonIgnore] public float ShardReward => _shardReward;
        [JsonIgnore] public float SaucerReward => _saucerReward;
        
        public GameConfig(float mapSize, float asteroidReward, float shardReward, float saucerReward)
        {
            _mapSize = mapSize;
            _asteroidReward = asteroidReward;
            _shardReward = shardReward;
            _saucerReward = saucerReward;
        }
    }
}