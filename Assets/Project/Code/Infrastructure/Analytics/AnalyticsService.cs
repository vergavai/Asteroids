using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Project.Code.Gameplay.Enemies.Type;
using UnityEngine;

namespace Project.Code.Infrastructure.Analytics
{
    public class AnalyticsService
    {
        private bool _isInitialized;
        private bool _isAvailable;

        public async UniTask Initialize()
        {
            if (_isInitialized) return;

            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (dependencyStatus == DependencyStatus.Available)
            {
                _isAvailable = true;
                Debug.Log("Firebase initialized");
            }
            else
            {
                _isAvailable = false;
                Debug.LogError($"Firebase initialization failed: {dependencyStatus}");
            }

            _isInitialized = true;
        }

        public void LogEnemySpawned(EnemyType type)
        {
            string enemyTypeString = MapEnemyTypeToString(type);
            LogEvent("SPAWNED_ENEMY", "ENEMY_TYPE", enemyTypeString);
        }

        private void LogEvent(string eventName, string parameterName, string parameterValue)
        {
            if (!_isInitialized)
            {
                Debug.LogWarning("AnalyticsService not initialized. Call Initialize() first.");
                return;
            }

            if (!_isAvailable)
            {
                Debug.LogWarning($"Firebase not available. Event '{eventName}' not logged.");
                return;
            }

            FirebaseAnalytics.LogEvent(eventName, new Parameter(parameterName, parameterValue));
            Debug.Log($"{eventName} : {parameterName} : {parameterValue}");
        }

        private string MapEnemyTypeToString(EnemyType type)
        {
            return type switch
            {
                EnemyType.Asteroid => "ASTEROID",
                EnemyType.Saucer => "SAUCER",
                _ => "UNKNOWN"
            };
        }
    }
}