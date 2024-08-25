namespace Features.Product.Data
{
    using Features.Data;
    using Features.Extensions.Data.UpdateableParam;
    using Features.Fruit.Data;
    using UnityEngine;

    /// <summary>
    /// Product data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ProductData), menuName = "Features/Product/Data/" + nameof(ProductData))]
    public class ProductData : StoreableSOWithSprites
    {
        [SerializeField]
        protected IntExponentialUpdateableParam fruitsCount = new IntExponentialUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 1
        };
        
        [SerializeField]
        protected IntUpdateableParam priceRatio = new IntUpdateableParam()
        {
            ParamValue = 2,
            Ratio = 1
        };

        /// <summary>
        /// Fruits count per product
        /// </summary>
        public virtual int GetFruitsCount(int level) => fruitsCount.GetGrowthValue(level);

        /// <summary>
        /// Product price (without fruits)
        /// </summary>
        public virtual int GetPrice(int priceLevel) => priceRatio.GetGrowthValue(priceLevel);
    }
}