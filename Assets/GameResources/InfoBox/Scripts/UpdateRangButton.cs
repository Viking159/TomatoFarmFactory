namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Update rang button
    /// </summary>
    public class UpdateRangButton : AbstractButtonView
    {
        [SerializeField]
        protected DoubleStoreableSO data = default;
        [SerializeField]
        protected MoneyData moneyData = default;

        protected override void OnButtonClick()
        {
            if (!CheckConditions())
            {
                return;
            }
            moneyData.SetCoins(moneyData.Coins - data.UpdateRangPrice);
            data.SetRang(data.Rang + 1);
        }

        protected virtual bool CheckConditions()
            => moneyData.Coins >= data.UpdateRangPrice;
    }
}