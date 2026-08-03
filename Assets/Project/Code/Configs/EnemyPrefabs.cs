using UnityEngine;

namespace Project.Code.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 51)]
    public class EnemyPrefabs : ScriptableObject
    {
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private GameObject _saucerPrefab;
        [SerializeField] private GameObject _shardPrefab;
        
        public GameObject AsteroidPrefab => _asteroidPrefab;
        public GameObject SaucerPrefab => _saucerPrefab;
        public GameObject ShardPrefab => _shardPrefab;
    }
}