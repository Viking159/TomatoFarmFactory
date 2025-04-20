namespace Features.AdsControl
{
    using CAS.AdObject;
    using System;
    using UnityEngine;

    [DefaultExecutionOrder(-100)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RewardedAdObject))]
    public sealed class RewardedInterstitialController : MonoBehaviour
    {
        public event Action onShow = delegate { };
        public event Action onClose = delegate { };
        public event Action<string> onFailedLoad = delegate { };
        public event Action<string> onFailedShow = delegate { };
        public event Action onReward = delegate { };

        public static RewardedInterstitialController Instance { get; private set; } = default;

        [SerializeField]
        private bool _showLogs = true;
        private RewardedAdObject _rewardedAd = default;

        /// <summary>
        /// Show rewarded video
        /// </summary>
        public void Present()
        {
            if (_rewardedAd.isAdReady)
            {
                _rewardedAd.Present();
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
            _rewardedAd = GetComponent<RewardedAdObject>();
            Subscribe();
        }

        private void Subscribe()
        {
            _rewardedAd.OnAdShown.AddListener(NotifyOnShow);
            _rewardedAd.OnAdClosed.AddListener(NotifyOnClose);
            _rewardedAd.OnReward.AddListener(NotifyOnReward);
            _rewardedAd.OnAdFailedToLoad.AddListener(NotifyOnLoadFailed);
            _rewardedAd.OnAdFailedToShow.AddListener(NotifyOnShowFailed);
        }

        private void Unsubscribe()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.OnAdShown.RemoveListener(NotifyOnShow);
                _rewardedAd.OnAdClosed.RemoveListener(NotifyOnClose);
                _rewardedAd.OnReward.RemoveListener(NotifyOnReward);
                _rewardedAd.OnAdFailedToLoad.RemoveListener(NotifyOnLoadFailed);
                _rewardedAd.OnAdFailedToShow.RemoveListener(NotifyOnShowFailed);
            }
        }

        private void OnDestroy() => Unsubscribe();

        private void NotifyOnShow()
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(RewardedInterstitialController)}: onShow");
            }
            onShow();
        }

        private void NotifyOnClose()
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(RewardedInterstitialController)}: onClose");
            }
            onClose();
        }

        private void NotifyOnReward()
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(RewardedInterstitialController)}: onReward");
            }
            onReward();
        }

        private void NotifyOnLoadFailed(string errorText)
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(RewardedInterstitialController)}: onFailedLoad: {errorText}");
            }
            onFailedLoad(errorText);
        }

        private void NotifyOnShowFailed(string errorText)
        {
            if (_showLogs)
            {
                Debug.Log($"{nameof(RewardedInterstitialController)}: onFailedShow: {errorText}");
            }
            onFailedShow(errorText);
        }
    }
}
