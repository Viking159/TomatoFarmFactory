namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    public class ConveyorUpdateLevelButton : AbstractButtonView
    {
        [SerializeField]
        protected ConveyorInfoBox infoBox = default;
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
            moneyData.SetCoins(moneyData.Coins - data.GetUpdateLevelPrice(infoBox.ConveyorController.Level));
            infoBox.ConveyorController.SetLevel(infoBox.ConveyorController.Level + 1);
        }

        protected virtual bool CheckConditions()
            => infoBox.ConveyorController != null && moneyData.Coins >= data.GetUpdateLevelPrice(infoBox.ConveyorController.Level)
            && infoBox.ConveyorController.Level < data.MaxLevel;
    }
}