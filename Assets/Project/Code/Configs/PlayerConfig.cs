using System;
using Unity.Plastic.Newtonsoft.Json;

namespace Project.Code.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        [JsonProperty("bulletCount")] private int _bulletCount;
        [JsonProperty("bulletSpeed")] private float _bulletSpeed;
        [JsonProperty("laserCount")] private int _laserCount;
        [JsonProperty("laserCooldown")] private float _laserCooldown;
        [JsonProperty("laserDuration")] private float _laserDuration;
        [JsonProperty("movementSpeed")] private float _movementSpeed;
        [JsonProperty("accelerationTime")] private float _accelerationTime;
        [JsonProperty("decelerationTime")] private float _decelerationTime;
        [JsonProperty("pushDecelerationTime")] private float _pushDecelerationTime;
        [JsonProperty("laserMaxCharges")] private int _laserMaxCharges;
        [JsonProperty("hearts")] private int _hearts;
        [JsonProperty("pushSpeed")] private float _pushSpeed;
        [JsonProperty("LaserLength")] private float _laserLength;
        [JsonProperty("LaserHeight")] private float _laserHeight;

        [JsonIgnore] public int BulletCount => _bulletCount;
        [JsonIgnore] public float BulletSpeed => _bulletSpeed;
        [JsonIgnore] public int LaserCount => _laserCount;
        [JsonIgnore] public float LaserCooldown => _laserCooldown;
        [JsonIgnore] public float LaserDuration => _laserDuration;
        [JsonIgnore] public float MovementSpeed => _movementSpeed;
        [JsonIgnore] public float AccelerationTime => _accelerationTime;
        [JsonIgnore] public float DecelerationTime => _decelerationTime;
        [JsonIgnore] public float PushDecelerationTime => _pushDecelerationTime;
        [JsonIgnore] public int LaserMaxCharges => _laserMaxCharges;
        [JsonIgnore] public float LaserHeight => _laserHeight;
        [JsonIgnore] public float LaserLength => _laserLength;
        [JsonIgnore] public int Hearts => _hearts;

        public float PushSpeed => _pushSpeed;

        public PlayerConfig(int bulletCount, float bulletSpeed, int laserCount, float laserCooldown,
            float laserDuration, float movementSpeed, float accelerationTime, float decelerationTime, float pushDecelerationTime,
            int laserMaxCharges, int hearts, float pushSpeed,  float laserLength, float laserHeight)
        {
            _bulletCount = bulletCount;
            _bulletSpeed = bulletSpeed;
            _laserCount = laserCount;
            _laserCooldown = laserCooldown;
            _laserDuration = laserDuration;
            _movementSpeed = movementSpeed;
            _accelerationTime = accelerationTime;
            _decelerationTime = decelerationTime;
            _pushDecelerationTime = pushDecelerationTime;   
            _laserMaxCharges = laserMaxCharges;
            _hearts = hearts;
            _pushSpeed = pushSpeed;
            _laserLength = laserLength;
            _laserHeight = laserHeight;
        }
    }
}