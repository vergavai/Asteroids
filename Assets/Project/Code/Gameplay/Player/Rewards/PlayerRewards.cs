using System.Collections.Generic;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies.Type;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Rewards
{
    public class PlayerRewards
    {
        private float _points;
        private GameConfig _config;
        private Dictionary<EnemyType, float> _rewards;

        public float Points => _points;

        public PlayerRewards(GameConfig config)
        {
            _config = config;

            _rewards = new Dictionary<EnemyType, float>
            {
                [EnemyType.Asteroid] = _config.AsteroidReward,
                [EnemyType.Shard] = _config.ShardReward,
                [EnemyType.Saucer] = _config.SaucerReward,
            };
        }

        public void Reward(EnemyType type)
        {
            _points += _rewards[type];
            Debug.Log($"Points: {_points}");
        }
    }
}