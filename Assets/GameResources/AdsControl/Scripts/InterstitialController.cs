namespace Features.AdsControl
{
    using CAS.AdObject;
    using System;
    using UnityEngine;

    [DefaultExecutionOrder(-100)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(InterstitialAdObject))]
    public sealed class InterstitialController : MonoBehaviour
    {
        public event Action onShow = delegate { };
        public event Action onClose = delegate { };
        public event Action<string> onFailedLoad = delegate { };
        public event Action<string> onFailedShow = delegate { };

        public static InterstitialController Instance { get; private set; } = default;

        public bool IsReady => _interstitialAd.isAdReady;

        [SerializeField]
        private bool _showLogs = true;
        private InterstitialAdObject _interstitialAd = default;

        /// <summary>
        /// Show interstitial
        /// </summary>
        public void Present()
        {
            if (_interstitialAd.isAdReady)
            {
                _interstitialAd.Present();
            }
            else
            {
                NotifyOnShowFailed(AdsController.NOT_READY_ERROR);
            }
        }

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
            _interstitialAd = GetComponent<InterstitialAdObject>();
            Subscribe();
        }

        private void Subscribe()
        {
            _interstitialAd.OnAdShown.AddListener(NotifyOnShow);
            _interstitialAd.OnAdClosed.AddListener(NotifyOnClose);
            _interstitialAd.OnAdFailedToLoad.AddListener(NotifyOnLoadFailed);
            _interstitialAd.OnAdFailedToShow.AddListener(NotifyOnShowFailed);
        }

        private void Unsubscribe()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.OnAdShown.RemoveListener(NotifyOnShow);
                _interstitialAd.OnAdClosed.RemoveListener(NotifyOnClose);
                _interstitialAd.OnAdFailedToLoad.RemoveListener(NotifyOnLoadFailed);
                _interstitialAd.OnAdFailedToShow.RemoveListener(NotifyOnShowFailed);
            }
        }

        private void OnDestroy() => Unsubscribe();

        private void NotifyOnShow()
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(InterstitialController)}: onShow");
            }
            onShow();
        }

        private void NotifyOnClose()
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(InterstitialController)}: onClose");
            }
            onClose();
        }

        private void NotifyOnLoadFailed(string errorText)
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(InterstitialController)}: onFailedLoad: {errorText}");
            }
            onFailedLoad(errorText);
        }

        private void NotifyOnShowFailed(string errorText)
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(InterstitialController)}: onFailedShow: {errorText}");
            }
            onFailedShow(errorText);
        }
    }
}
