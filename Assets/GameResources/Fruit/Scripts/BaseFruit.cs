namespace Features.Fruit
{
    using Features.Data;
    using Features.Fruit.Data;
    using System;
    using UnityEngine;
    using Features.Interfaces;

    /// <summary>
    /// Base fruit class
    /// </summary>
    public class BaseFruit : AbstractStoredDataController<FruitStoredData>, ISaleable, IConsumable
    {
        /// <summary>
        /// Level change event
        /// </summary>
        public event Action onLevelChange = delegate { };

        /// <summary>
        /// Fruit name
        /// </summary>
        public virtual string Name => _fruitStoredData.Name;

        /// <summary>
        /// Fruits count
        /// </summary>
        public virtual int Count => _count;

        /// <summary>
        /// Fruit level
        /// </summary>
        public virtual int Level => _fruitStoredData.Level;

        [SerializeField]
        protected FruitData _fruitData = default;
        protected FruitStoredData _fruitStoredData = default;
        protected int _count = 0;

        protected const string PP_KEY = "baseFruitDataPPKey";

        private void Awake()
        {
            InitData();
        }

        public virtual void Sale()
        {
            Debug.Log($"{nameof(BaseFruit)}: Sale");
        }

        public virtual void Consume(IConsumer consumer)
        {
            Debug.Log($"{nameof(BaseFruit)}: Consume");
        }

        protected override void InitData()
        {
            _fruitStoredData = LoadData(GetPPKey());
            if (_fruitStoredData == null)
            {
                _fruitStoredData = new FruitStoredData()
                {
                    Name = _fruitData.Name.DataValue,
                    Price = _fruitData.Price.DataValue,
                    Level = 0
                };
            }
        }

        protected virtual string GetPPKey()
            => PP_KEY + _fruitData.Name.DataValue;

        protected virtual void OnDestroy()
        {
            SaveData(GetPPKey(), _fruitStoredData);
        }
    }
}