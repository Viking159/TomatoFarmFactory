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
        /// <summary>
        /// Fruits count per product
        /// </summary>
        public virtual int FruitsCount => fruitsCount.GetGrowthValue(level);
        [SerializeField]
        protected IntExponentialUpdateableParam fruitsCount = new IntExponentialUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 1
        };

        /// <summary>
        /// Product price
        /// </summary>
        public virtual int Price => fruitData.Count * fruitData.Price * priceRatio.GetGrowthValue(priceLevel);
        [SerializeField]
        protected IntUpdateableParam priceRatio = new IntUpdateableParam()
        {
            ParamValue = 2,
            Ratio = 1
        };

        [SerializeField]
        protected int priceLevel = default;

        [SerializeField]
        protected FruitData fruitData = default;

        /// <summary>
        /// Set price level
        /// </summary>
        public virtual void SetPriceLevel(int val)
            => priceLevel = Mathf.Max(0, val);
    }
}