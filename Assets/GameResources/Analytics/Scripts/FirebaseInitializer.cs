namespace Features.Analytics
{
    using System.Collections;
    using UnityEngine;
    using Firebase;
    using Firebase.Analytics;
    using System;

    public class FirebaseInitializer : MonoBehaviour
    {
        public  event Action onDependencyStatusChange = delegate { };
        public DependencyStatus DependencyStatusValue { get; private set; } = DependencyStatus.UnavailableUpdaterequired;

        public static FirebaseInitializer Instance { get; private set; } = default;

        private DependencyStatus  tempDependencyStatus;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(CheckAndFixDependencies());
        }

        private IEnumerator CheckAndFixDependencies()
        {
            var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
            yield return new WaitUntil(() => dependencyTask.IsCompleted);

            DependencyStatusValue = dependencyTask.Result;
            if (DependencyStatusValue == DependencyStatus.Available)
            {
                Debug.Log("Firebase initialized successfully!");
                InitializeAnalytics();
            }
            else
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {DependencyStatusValue}");
            }
            onDependencyStatusChange();
        }

        private void InitializeAnalytics()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
        }
    }
}