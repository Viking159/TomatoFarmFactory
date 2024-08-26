namespace Features.FruitsStartCount
{
    using Features.AbstractFloatRatio;
    using Features.Fruit.Data;
    public class FruitsStartCount : AbstractIntSet<FruitData>
    {
        protected override int paramValue => data.FruitsCount.ParamValue;

        protected override void SetData(int res)
        {
            data.SetStartCount(res);
        }
    }
}