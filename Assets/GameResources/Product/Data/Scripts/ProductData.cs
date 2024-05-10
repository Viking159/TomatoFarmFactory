namespace Features.Product.Data
{
    using Features.Data;
    using Features.Extensions.Data.UpdateableParam;
    using UnityEngine;

    /// <summary>
    /// Product data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ProductData), menuName = "Features/Product/Data/" + nameof(ProductData))]
    public class ProductData : StoreableSO
    {
        /// <summary>
        /// Fruits count per product
        /// </summary>
        public virtual int FruitsCount => price.GetGrowthValue(level);
        [SerializeField]
        protected IntExponentialUpdateableParam fruitsCount = new IntExponentialUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 1
        };

        /// <summary>
        /// Product price
        /// </summary>
        public virtual int Price => price.GetGrowthValue(priceLevel);
        [SerializeField]
        protected IntUpdateableParam price = new IntUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 1
        };

        [SerializeField]
        protected int priceLevel = default;

        /// <summary>
        /// Set price level
        /// </summary>
        public virtual void SetPriceLevel(int val)
            => priceLevel = Mathf.Max(0, val);
    }
}