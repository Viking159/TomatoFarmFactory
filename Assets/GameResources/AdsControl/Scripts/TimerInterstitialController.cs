namespace Features.AdsControl
{
    using System.Collections;
    using UnityEngine;

    public  class TimerInterstitialController : AbstractAdsProvider
    {
        [SerializeField, Min(1)]
        protected float awaitSeconds = 10;
        protected Coroutine timerCoroutine = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            InterstitialController.Instance.onClose += StartTimer;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            StopCoroutine();
            InterstitialController.Instance.onClose -= StartTimer;
        }

        protected virtual IEnumerator ShowAfterTimer()
        {
            yield return new WaitForSeconds(awaitSeconds);
            InterstitialController.Instance.Present();
        }

        protected override void HandleAdsInit() => StartTimer();

        protected override void HandleAdsInitFail(string errorText) { }

        protected virtual void StartTimer()
        {
            StopCoroutine();
            timerCoroutine = StartCoroutine(ShowAfterTimer());
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
