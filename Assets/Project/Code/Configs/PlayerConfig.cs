using System;
using Unity.Plastic.Newtonsoft.Json;

namespace Project.Code.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        [JsonProperty("BulletCount")] private int _bulletCount;
        [JsonProperty("BulletSpeed")] private float _bulletSpeed;
        [JsonProperty("LaserCount")] private int _laserCount;
        [JsonProperty("LaserCooldown")] private float _laserCooldown;
        [JsonProperty("LaserDuration")] private float _laserDuration;
        [JsonProperty("MovementSpeed")] private float _movementSpeed;
        [JsonProperty("AccelerationTime")] private float _accelerationTime;
        [JsonProperty("DecelerationTime")] private float _decelerationTime;
        [JsonProperty("PushDecelerationTime")] private float _pushDecelerationTime;
        [JsonProperty("LaserMaxCharges")] private int _laserMaxCharges;
        [JsonProperty("Hearts")] private int _hearts;
        [JsonProperty("PushSpeed")] private float _pushSpeed;
        [JsonProperty("LaserLength")] private float _laserLength;
        [JsonProperty("LaserHeight")] private float _laserHeight;
        [JsonProperty("InvincibilityDuration")] private float _invincibilityDuration;

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
        [JsonIgnore] public float InvincibilityDuration => _invincibilityDuration;
        [JsonIgnore] public float PushSpeed => _pushSpeed;

        public PlayerConfig(int bulletCount, float bulletSpeed, int laserCount, float laserCooldown,
            float laserDuration, float movementSpeed, float accelerationTime, float decelerationTime, float pushDecelerationTime,
            int laserMaxCharges, int hearts, float pushSpeed,  float laserLength, float laserHeight, float invincibilityDuration)
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
            _invincibilityDuration = invincibilityDuration;
        }
    }
}