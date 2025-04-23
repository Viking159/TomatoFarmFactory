namespace Features.InfoBox
{
    using Features.AdsControl;
    using Features.Store;

    public class AdsUpdateRangButton : UpdateRangButton
    {
        protected override void OnButtonClick()
        {
            if (CheckConditions() && RewardedInterstitialController.Instance != null)
            {
                RewardedInterstitialController.Instance.onReward += HandleReward;
                RewardedInterstitialController.Instance.onClose += Unsubcribe;
                RewardedInterstitialController.Instance.onFailedLoad += HandleFail;
                RewardedInterstitialController.Instance.onFailedShow += HandleFail;
                RewardedInterstitialController.Instance.Present();
            }
        }

        private void HandleReward()
        {
            base.OnButtonClick();
            Unsubcribe();
        }

        protected virtual void HandleFail(string errorText) => Unsubcribe();

        protected virtual void Unsubcribe()
        {
            if (RewardedInterstitialController.Instance != null)
            {
                RewardedInterstitialController.Instance.onReward -= HandleReward;
                RewardedInterstitialController.Instance.onClose -= Unsubcribe;
                RewardedInterstitialController.Instance.onFailedLoad -= HandleFail;
                RewardedInterstitialController.Instance.onFailedShow -= HandleFail;
            }
            
        }

        protected virtual void OnDisable() => Unsubcribe();
    }
}