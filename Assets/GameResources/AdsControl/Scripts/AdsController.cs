namespace Features.AdsControl
{
    using CAS.AdObject;
    using System;
    using UnityEngine;

    [DefaultExecutionOrder(-100)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ManagerAdObject))]
    public class AdsController : MonoBehaviour
    {
        public event Action onInit = delegate { };
        public event Action<string> onInitFailed = delegate { };

        public static AdsController Instance { get; private set; } = default;
        public bool IsInited { get; private set; } = false;
        public string FailText { get; private set; } = string.Empty;

        [SerializeField]
        private bool _showLogs = true;
        private ManagerAdObject _adsManager = default;

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
            _adsManager = GetComponent<ManagerAdObject>();
            Subscribe();
        }

        private void OnDestroy() => Unsubscribe();

        private void Subscribe()
        {
            _adsManager.OnInitialized.AddListener(NotifyOnInit);
            _adsManager.OnInitializationFailed.AddListener(NotifyOnInitFailed);
        }

        private void Unsubscribe()
        {
            if (_adsManager != null)
            {
                _adsManager.OnInitialized.RemoveListener(NotifyOnInit);
                _adsManager.OnInitializationFailed.RemoveListener(NotifyOnInitFailed);
            }
        }

        private void NotifyOnInit()
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(AdsController)}: onInit");
            }
            IsInited = true;
            onInit();
        }

        private void NotifyOnInitFailed(string errorText)
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(AdsController)}: onInitFailed: {errorText}");
            }
            IsInited = false;
            FailText = errorText;
            onInitFailed(errorText);
        }
    }
}
