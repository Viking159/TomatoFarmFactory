namespace Features.AdsControl
{
    using UnityEngine;

    public abstract class AbstractAdsProvider : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            if (AdsController.Instance.IsInited)
            {
                HandleAdsInit();
                return;
            }
            if (!string.IsNullOrEmpty(AdsController.Instance.FailText))
            {
                HandleAdsInitFail(AdsController.Instance.FailText);
                return;
            }
            AdsController.Instance.onInit += HandleAdsInit;
            AdsController.Instance.onInitFailed += HandleAdsInitFail;
        }

        protected abstract void HandleAdsInit();
        protected abstract void HandleAdsInitFail(string errorText);

        protected virtual void OnDisable()
        {
            AdsController.Instance.onInit += HandleAdsInit;
            AdsController.Instance.onInitFailed += HandleAdsInitFail;
        }
    }
}
