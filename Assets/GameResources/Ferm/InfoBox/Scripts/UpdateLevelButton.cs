namespace Features.Ferm.InfoBox
{
    using Features.Ferm.Data;

    /// <summary>
    /// Update level button
    /// </summary>
    public class UpdateLevelButton : AbstractUpdateButton<FermData>
    {
        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            moneyData.SetCoins(moneyData.Coins - data.UpdateLevelPrice);
            data.SetLevel(data.Level + 1);
        }

        protected override bool CheckConditions()
            => moneyData.Coins >= data.UpdateLevelPrice;
    }
}