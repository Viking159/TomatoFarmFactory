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
                if (baseCreatorInfoBox != null )
                {
                    Debug.Log($"UpdateLevelButton: CheckConditions false. baseCreatorInfoBox.SpawnerData == null ?: {baseCreatorInfoBox.SpawnerData == null}");
                }
                else
                {
                    Debug.Log($"UpdateLevelButton: CheckConditions false. baseCreatorInfoBox is null");
                }
                return;
            }
            moneyData.SetCoins(moneyData.Coins - data.GetUpdateLevelPrice(baseCreatorInfoBox.SpawnerData.Level));
            baseCreatorInfoBox.Creator.SetLevel(baseCreatorInfoBox.SpawnerData.Level + 1);
        }

        protected virtual bool CheckConditions()
            => baseCreatorInfoBox != null && baseCreatorInfoBox.SpawnerData != null && moneyData.Coins >= data.GetUpdateLevelPrice(baseCreatorInfoBox.SpawnerData.Level)
            && baseCreatorInfoBox.SpawnerData.Level < data.MaxLevel;
    }
}