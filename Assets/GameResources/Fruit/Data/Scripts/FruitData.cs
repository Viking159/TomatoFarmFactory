namespace Features.Fruit.Data
{
    using Extensions.UpdateableParam;
    using UnityEngine;

    /// <summary>
    /// Fruit data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FruitData), menuName = "Features/Data/Fruit/" + nameof(FruitData))]
    public sealed class FruitData : ScriptableObject
    {
        /// <summary>
        /// Fruit name
        /// </summary>
        public string Name => _name;
        [SerializeField]
        private string _name = default;

        /// <summary>
        /// Fruit price
        /// </summary>
        public float Price => _price.ParamValue;
        [SerializeField]
        private FloatUpdateableParam _price = default;

        public int Level => _level.ParamValue;
        [SerializeField]
        private IntUpdateableParam _level = default;
    }
}