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
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
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
            moneyData.SetCoins(moneyData.Coins - data.GetUpdateRangPrice(baseCreatorInfoBox.SpawnerData.Rang));
            baseCreatorInfoBox.Creator.SetRang(baseCreatorInfoBox.SpawnerData.Rang + 1);
        }

        protected virtual bool CheckConditions()
            => moneyData.Coins >= data.GetUpdateRangPrice(baseCreatorInfoBox.SpawnerData.Rang)
            && baseCreatorInfoBox.SpawnerData.Rang < data.MaxRang;
    }
}