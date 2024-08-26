namespace Features.ProductFruitRatio
{
    using Features.AbstractFloatStart;
    using Features.Product.Data;

    public class ProductFruitRatio : AbstractFloatSet<ProductData>
    {
        protected override float paramValue => data.FruitsCount.Ratio;

        protected override void SetData(float res)
        {
            data.SetRatio(res);
        }
    }
}