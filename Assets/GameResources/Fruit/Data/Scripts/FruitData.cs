namespace Features.Fruit.Data
{
    using Extensions.UpdateableParam;
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Fruit data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FruitData), menuName = "Features/Data/Fruit/" + nameof(FruitData))]
    public class FruitData : StoreableSO
    {
        /// <summary>
        /// Fruit name
        /// </summary>
        public virtual string Name => fruitName;
        [SerializeField]
        protected string fruitName = default;

        /// <summary>
        /// Fruit price
        /// </summary>
        public virtual int Price => price.GetGrowthValue(level);
        [SerializeField]
        protected IntUpdateableParam price = new IntUpdateableParam()
        {
            ParamValue = 1,
            GrowthPercent = 100
        };

        /// <summary>
        /// Fruits count
        /// </summary>
        public int Count => count.GetGrowthValue(level);
        protected IntUpdateableParam count = new IntUpdateableParam()
        {
            ParamValue = 1,
            GrowthPercent = 100
        };
    }
}