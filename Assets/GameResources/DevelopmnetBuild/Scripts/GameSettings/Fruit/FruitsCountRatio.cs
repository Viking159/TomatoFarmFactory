namespace Features.FruitsCountRatio
{
    using Features.AbstractFloatStart;
    using Features.Fruit.Data;

    public class FruitsCountRatio : AbstractFloatSet<FruitData>
    {
        protected override float paramValue => data.FruitsCount.Ratio;

        protected override void SetData(float res)
        {
            data.SetRatio(res);
        }
    }
}