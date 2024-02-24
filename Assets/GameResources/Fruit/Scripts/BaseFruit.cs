namespace Features.Fruit
{
    using Features.Fruit.Data;
    using System;
    using UnityEngine;
    using Features.Interfaces;
    using Features.Data.BaseContainerData;

    /// <summary>
    /// Base fruit class
    /// </summary>
    public class BaseFruit : MonoBehaviour, ISaleable, IConsumable
    {
        /// <summary>
        /// Level change event
        /// </summary>
        public event Action onLevelChange = delegate { };

        /// <summary>
        /// Fruit name
        /// </summary>
        public virtual StringReadonlyData Name => _fruitData.Name;

        /// <summary>
        /// Fruits count
        /// </summary>
        public virtual int Count => _count;

        /// <summary>
        /// Fruit level
        /// </summary>
        public virtual int Level => _level;

        [SerializeField]
        protected FruitData _fruitData = default;
        protected int _level = 0;
        protected int _count = 0;

        public virtual void Sale()
        {
            Debug.Log($"{nameof(BaseFruit)}: Sale");
        }

        public virtual void Consume(IConsumer consumer)
        {
            Debug.Log($"{nameof(BaseFruit)}: Consume");
        }
    }
}