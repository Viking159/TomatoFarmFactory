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
        [SerializeField]
        protected IntUpdateableParam count = new IntUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 100
        };

        
        [SerializeField]
        protected IntUpdateableParam price = new IntUpdateableParam()
        {
            ParamValue = 1,
            Ratio = 100
        };

        /// <summary>
        /// Fruits count
        /// </summary>
        public virtual int GetCount(int level) => count.GetGrowthValue(level);

        /// <summary>
        /// Fruit price
        /// </summary>
        public virtual int GetPrice(int priceLevel) => price.GetGrowthValue(priceLevel);
    }
}