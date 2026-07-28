using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Project.Code.Infrastructure.Analytics
{
    public class Analytics
    {
        public async UniTask Initialize()
        {
            await FirebaseApp
                .CheckAndFixDependenciesAsync()
                .ContinueWith(task =>
                    {
                        DependencyStatus dependencyStatus = task.Result;

                        if (dependencyStatus == DependencyStatus.Available)
                        {
                            Debug.Log("Firebase initialized");
                        }
                        else
                        {
                            Debug.LogError($"Firebase is not initialized: {dependencyStatus}");
                        }
                    }
                );
        }

        public void LogEvent(string eventName, string parameterName, string parameterValue)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var status = task.Result;
                if (status == DependencyStatus.Available) {
                    FirebaseAnalytics.LogEvent(eventName, new Parameter(parameterName, parameterValue));
                } else {
                    Debug.LogWarning("Firebase not ready in Editor: " + status);
                }
            });
            
            Debug.Log(eventName + " : " + parameterName + " : " + parameterValue);
        }
    }
}