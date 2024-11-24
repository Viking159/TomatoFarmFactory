namespace Features.Conveyor
{
    using Features.Data;
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Update conveyor level button
    /// </summary>
    public class UpdateConveyorButton : AbstractButtonView
    {
        [SerializeField]
        protected ConveyorController conveyorController = default;
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
            moneyData.SetCoins(moneyData.Coins - data.GetUpdateLevelPrice(conveyorController.Level));
            conveyorController.SetLevel(conveyorController.Level + 1);
        }

        protected virtual bool CheckConditions()
            => moneyData.Coins >= data.GetUpdateLevelPrice(conveyorController.Level);
    }
}