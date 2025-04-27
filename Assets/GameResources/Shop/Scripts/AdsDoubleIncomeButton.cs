namespace Features.Shop
{
    using Features.AdsControl;
    using Features.Extensions.View;
    using Features.IncomeTextAnimation;
    using Features.InfoBox;
    using Features.Shop.Data;
    using UnityEngine;

    public class AdsDoubleIncomeButton : AbstractButtonView
    {
        [SerializeField]
        protected MoneyData moneyData = default;
        [SerializeField]
        protected IncomeInfoBox incomeInfoBox = default;
        [SerializeField]
        protected IncomeTextAnimation incomeTextAnimation = default;

        protected override void OnButtonClick()
        {
            if (RewardedInterstitialController.Instance != null)
            {
                RewardedInterstitialController.Instance.onReward += DoubleReward;
                RewardedInterstitialController.Instance.onClose += Unsubcribe;
                RewardedInterstitialController.Instance.onFailedLoad += HandleFail;
                RewardedInterstitialController.Instance.onFailedShow += HandleFail;
                RewardedInterstitialController.Instance.Present();
            }
        }

        protected virtual void HandleFail(string errorText) => Unsubcribe();

        protected virtual void DoubleReward()
        {
            Unsubcribe();
            moneyData.SetCoins(moneyData.Coins + incomeInfoBox.Income);
            incomeTextAnimation.AnimateText(incomeInfoBox.Income);
            gameObject.SetActive(false);
        }

        protected virtual void Unsubcribe()
        {
            if (RewardedInterstitialController.Instance != null)
            {
                RewardedInterstitialController.Instance.onReward -= DoubleReward;
                RewardedInterstitialController.Instance.onClose -= Unsubcribe;
                RewardedInterstitialController.Instance.onFailedLoad -= HandleFail;
                RewardedInterstitialController.Instance.onFailedShow -= HandleFail;
            }
        }

        protected virtual void OnDisable() => Unsubcribe();
    }
}
