namespace Features.FruitsPrice
{
    using Features.AbstractFloatRatio;
    using Features.Fruit.Data;

    public class FruitsPrice : AbstractIntSet<FruitData>
    {
        protected override int paramValue => data.PriceRatio.ParamValue;

        protected override void SetData(int res)
        {
            data.SetPrice(res);
        }
    }
}