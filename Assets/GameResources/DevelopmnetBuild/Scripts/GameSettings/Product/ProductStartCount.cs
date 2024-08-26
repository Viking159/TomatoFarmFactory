namespace Features.ProductStartCount
{
    using Features.AbstractFloatRatio;
    using Features.Product.Data;

    public class ProductStartCount : AbstractIntSet<ProductData>
    {
        protected override int paramValue => data.FruitsCount.ParamValue;

        protected override void SetData(int res)
        {
            data.SetStartCount(res);
        }
    }
}