namespace Features.InfoBox
{
    using Features.AdsControl;

    public class AdsUpdateRangButton : UpdateRangButton
    {
        protected override void OnButtonClick()
        {
            if (CheckConditions())
            {
                RewardedInterstitialController.Instance.onReward += HandleReward;
                RewardedInterstitialController.Instance.Present();
            }
        }

        private void HandleReward()
        {
            base.OnButtonClick();
            Unsubcribe();
        }

        protected virtual void Unsubcribe() => RewardedInterstitialController.Instance.onReward -= HandleReward;

        protected virtual void OnDisable() => Unsubcribe();
    }
}