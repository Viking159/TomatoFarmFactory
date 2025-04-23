namespace Features.AdsControl
{
    using System;
    using System.Collections;
    using UnityEngine;

    public  class TimerInterstitialController : AbstractAdsProvider
    {
        protected const float MIN_AWAIT = 30;

        [SerializeField, Min(1)]
        protected float awaitSeconds = 10;
        [SerializeField]
        protected bool restartTimeOnReward = true;
        protected Coroutine timerCoroutine = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (InterstitialController.Instance != null)
            {
                InterstitialController.Instance.onClose += StartTimer;
                InterstitialController.Instance.onFailedShow += HandleFail;
                InterstitialController.Instance.onFailedLoad += HandleFail;
            }
            if (RewardedInterstitialController.Instance != null && restartTimeOnReward)
            {
                RewardedInterstitialController.Instance.onReward += StartTimer;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (InterstitialController.Instance != null)
            {
                InterstitialController.Instance.onClose -= StartTimer;
                InterstitialController.Instance.onFailedShow -= HandleFail;
                InterstitialController.Instance.onFailedLoad -= HandleFail;
            }
            if (RewardedInterstitialController.Instance != null && restartTimeOnReward)
            {
                RewardedInterstitialController.Instance.onReward -= StartTimer;
            }
            StopCoroutine();
        }

        protected virtual IEnumerator ShowAfterTimer(float awaitTime)
        {
            yield return new WaitForSeconds(awaitSeconds);
            if (InterstitialController.Instance != null)
            {
                InterstitialController.Instance.Present();
            }
        }

        protected override void HandleAdsInit() => StartTimer();

        protected override void HandleAdsInitFail(string errorText) { }

        protected virtual void StartTimer()
        {
            StopCoroutine();
            timerCoroutine = StartCoroutine(ShowAfterTimer(awaitSeconds));
        }

        private void HandleFail(string errorText)
        {
            StopCoroutine();
            timerCoroutine = StartCoroutine(ShowAfterTimer(MIN_AWAIT));
        }

        protected virtual void StopCoroutine()
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
        }
    }
}
