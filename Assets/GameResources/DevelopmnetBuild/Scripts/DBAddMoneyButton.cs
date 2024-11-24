namespace Features.DevelopmentBuild
{
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Add money button 
    /// </summary>
    /// <remarks>
    /// Only for development build
    /// </remarks>
    public class DBAddMoneyButton : AbstractButtonView
    {
        [SerializeField]
        protected MoneyData moneyData = default;
        [SerializeField]
        protected int coinsAdding = 100;

        protected override void OnButtonClick() 
            => moneyData.SetCoins(moneyData.Coins + coinsAdding);
    }
}