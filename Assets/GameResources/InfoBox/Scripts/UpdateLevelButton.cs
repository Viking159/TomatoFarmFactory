namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Update level button
    /// </summary>
    public class UpdateLevelButton : AbstractButtonView
    {
        [SerializeField]
        protected StoreableSO data = default;
        [SerializeField]
        protected MoneyData moneyData = default;

        protected override void OnButtonClick()
        {
            if (!CheckConditions())
            {
                return;
            }
            moneyData.SetCoins(moneyData.Coins - data.UpdateLevelPrice);
            data.SetLevel(data.Level + 1);
        }

        protected virtual bool CheckConditions()
            => moneyData.Coins >= data.UpdateLevelPrice;
    }
}