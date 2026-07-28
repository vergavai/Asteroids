using Unity.Plastic.Newtonsoft.Json;

namespace Project.Code.Configs
{
    [System.Serializable]
    public class EnemyConfig
    {
        [JsonProperty("AsteroidCount")] private int _asteroidCount;
        [JsonProperty("SaucerCount")] private int _saucerCount;
        [JsonProperty("SpawnCooldown")] private float _spawnCooldown;
        [JsonProperty("AsteroidSpeed")] private float _asteroidSpeed;
        [JsonProperty("SaucerSpeed")] private float _saucerSpeed;
        [JsonProperty("DisableRange")] private float _disableRange;
        [JsonProperty("ShardsPerAsteroid")] private int _shardsPerAsteroid;
        [JsonProperty("SaucerTrackInterval")] private float _saucerTrackInterval;

        [JsonIgnore] public int AsteroidCount => _asteroidCount;
        [JsonIgnore] public int SaucerCount => _saucerCount;
        [JsonIgnore] public float SpawnCooldown => _spawnCooldown;
        [JsonIgnore] public float AsteroidSpeed => _asteroidSpeed;
        [JsonIgnore] public float SaucerSpeed => _saucerSpeed;
        [JsonIgnore] public float DisableRange => _disableRange;
        [JsonIgnore] public int ShardsPerAsteroid => _shardsPerAsteroid;
        [JsonIgnore] public float SaucerTrackInterval => _saucerTrackInterval;

        public EnemyConfig(int asteroidCount, int saucerCount, float spawnCooldown, float asteroidSpeed,
            float saucerSpeed, float disableRange, int shardsPerAsteroid, float saucerTrackInterval)
        {
            _asteroidCount = asteroidCount;
            _saucerCount = saucerCount;
            _spawnCooldown = spawnCooldown;
            _asteroidSpeed = asteroidSpeed;
            _saucerSpeed = saucerSpeed;
            _disableRange = disableRange;
            _shardsPerAsteroid = shardsPerAsteroid;
            _saucerTrackInterval = saucerTrackInterval;
        }
    }
}