using Cysharp.Threading.Tasks;
using Project.Code.Common;
using Project.Code.Configs;
using Project.Code.Gameplay.Enemies.Asteroid;
using Project.Code.Gameplay.Player.Movement;
using Project.Code.Infrastructure.Analytics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Code.Gameplay.Enemies.EnemyGenerator
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float DisableRangePadding = 2f;
        
        private EnemiesPreparer _enemiesPreparer;
        private ObjectPool<EnemyBehaviour> _enemyPool;
        private ObjectPool<AsteroidShardBehaviour> _shardPool;
        private Camera _camera;
        private Transform _player;
        private AnalyticsService _analytics;
        
        private float _currentCooldown;
        private float _cooldown;
        private float _leftBound;
        private float _rightBound;
        private float _topBound;
        private float _bottomBound;
        private float _enemyDisableRange;

        [Inject]
        private void Construct(EnemiesPreparer creator, ObjectPool<EnemyBehaviour> pool, ObjectPool<AsteroidShardBehaviour> shardPool,
            Camera camera, EnemyConfig config, PlayerMovementBehaviour player, AnalyticsService analytics)
        {
            _enemiesPreparer = creator;
            _enemyPool = pool;
            _camera = camera;
            _cooldown = config.SpawnCooldown;
            _player = player.transform;
            _enemyDisableRange = config.DisableRange;
            _shardPool = shardPool;
            _analytics = analytics;

            _leftBound = _camera.ViewportToWorldPoint(new Vector3(0, 0.5f, -camera.transform.position.z)).x;
            _topBound = _camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, -camera.transform.position.z)).y;
            _rightBound = -_leftBound;
            _bottomBound = -_topBound;

            _enemiesPreparer.CreateAndAddObjects();
        }

        private void Start()
        {
            _analytics.Initialize().Forget();
        }

        private void Update()
        {
            _currentCooldown += Time.deltaTime;

            if (_currentCooldown >= _cooldown)
            {
                SpawnEnemy();
                _currentCooldown = 0f;
            }
            
            _enemyPool.DisableObjectsOutsideCamera(_enemyDisableRange);
            _shardPool.DisableObjectsOutsideCamera(_enemyDisableRange);
        }

        private void SpawnEnemy()
        {
            _enemyPool.TryGetRandomObject(out EnemyBehaviour enemy);

            if (!enemy || !_player)
                return;

            enemy.transform.position = GetRandomPointOutsideScreen(DisableRangePadding);

            if (enemy is AsteroidBehaviour asteroid)
            {
                asteroid.SetDirection((_player.position - enemy.transform.position).normalized);
            }
            
            _analytics.LogEnemySpawned(enemy.Type);
            enemy.gameObject.SetActive(true);
        }

        private Vector2 GetRandomPointOutsideScreen(float offset)
        {
            int side = Random.Range(0, 4);
            float x, y;

            switch (side)
            {
                case 0:
                    x = Random.Range(_leftBound - offset, _rightBound + offset);
                    y = _topBound + offset;
                    break;
                case 1: 
                    x = Random.Range(_leftBound - offset, _rightBound + offset);
                    y = _bottomBound - offset;
                    break;
                case 2:
                    x = _leftBound - offset;
                    y = Random.Range(_bottomBound - offset, _topBound + offset);
                    break;
                default: 
                    x = _rightBound + offset;
                    y = Random.Range(_bottomBound - offset, _topBound + offset);
                    break;
            }

            return new Vector2(x, y);
        }
    }
}