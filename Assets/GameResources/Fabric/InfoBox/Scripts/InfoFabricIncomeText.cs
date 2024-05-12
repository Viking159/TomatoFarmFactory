namespace Features.Fabric.InfoBox
{
    using Features.InfoBox;
    using Features.Product.Data;

    /// <summary>
    ///  Fabric income info text
    /// </summary>
    public class InfoFabricIncomeText : AbstractInfoText<ProductData>
    {
        protected override void SetText() 
            => SetView(string.Format(mask, data.Price * data.FruitsCount));
    }
}