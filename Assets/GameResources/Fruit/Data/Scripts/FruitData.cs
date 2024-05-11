namespace Features.Fruit.Data
{
    using Features.Extensions.Data.UpdateableParam;
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Fruit data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FruitData), menuName = "Features/Data/Fruit/" + nameof(FruitData))]
    public class FruitData : StoreableSOWithSprites
    {
        /// <summary>
        /// Fruits count
        /// </summary>
        public virtual int Count => count.GetGrowthValue(level);
        [SerializeField]
        protected IntUpdateableParam count = new IntUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 100
        };

        /// <summary>
        /// Fruit price
        /// </summary>
        public virtual int Price => price.GetGrowthValue(priceLevel);
        [SerializeField]
        protected IntUpdateableParam price = new IntUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 100
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