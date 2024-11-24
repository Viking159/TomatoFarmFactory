using Features.AbstractFloatRatio;
using Features.Product.Data;

namespace Features.ProductPrice
{

    public class ProductPrice : AbstractIntSet<ProductData>
    {
        protected override int paramValue => data.PriceRatio.ParamValue;

        protected override void SetData(int res)
        {
            data.SetPrice(res);
        }
    }
}