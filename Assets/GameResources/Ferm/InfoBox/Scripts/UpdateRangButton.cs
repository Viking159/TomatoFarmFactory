namespace Features.Ferm.InfoBox
{
    using Features.Ferm.Data;

    /// <summary>
    /// Update rang button
    /// </summary>
    public class UpdateRangButton : AbstractUpdateButton<FermData>
    {
        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            moneyData.SetCoins(moneyData.Coins - data.UpdateRangPrice);
            data.SetRang(data.Rang + 1);
        }

        protected override bool CheckConditions()
            => moneyData.Coins >= data.UpdateRangPrice;
    }
}