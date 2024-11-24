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
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
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
            moneyData.SetCoins(moneyData.Coins - data.GetUpdateLevelPrice(baseCreatorInfoBox.SpawnerData.Level));
            baseCreatorInfoBox.Creator.SetLevel(baseCreatorInfoBox.SpawnerData.Level + 1);
        }

        protected virtual bool CheckConditions()
            => moneyData.Coins >= data.GetUpdateLevelPrice(baseCreatorInfoBox.SpawnerData.Level);
    }
}