namespace Features.Ferm.InfoBox
{
    using Features.Fruit.Data;
    using Features.InfoBox;

    /// <summary>
    ///  Ferm income info text
    /// </summary>
    public class InfoFermIncomeText : AbstractInfoText<FruitData>
    {
        protected override void SetText()
            => SetView(string.Format(mask, data.Price * data.Count));
    }
}
