namespace Features.Fruit.Data
{
    using UnityEngine;
    using Features.Data.BaseContainerData;

    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FruitData), menuName = "Features/Data/Fruit/" + nameof(FruitData))]
    public sealed class FruitData : ScriptableObject
    {
        /// <summary>
        /// Fruit name
        /// </summary>
        public StringReadonlyData Name => _name;
        [SerializeField]
        private StringReadonlyData _name = default;

        /// <summary>
        /// Fruit price
        /// </summary>
        public IntReadonlyData Price => _price;
        [SerializeField]
        private IntReadonlyData _price = default;
    }
}