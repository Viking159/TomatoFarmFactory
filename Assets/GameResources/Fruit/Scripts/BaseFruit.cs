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
    public class BaseFruit : MonoBehaviour, ISaleable, IConsumable
    {
        public event Action onDataInited = delegate { };

        /// <summary>
        /// Fruit name
        /// </summary>
        public virtual string Name => fruitStoredData.Name;

        /// <summary>
        /// Fruit price
        /// </summary>
        public virtual float Price => fruitStoredData.Price;

        /// <summary>
        /// Fruit level
        /// </summary>
        public virtual int Level => fruitStoredData.Level;
       
        /// <summary>
        /// Fruits count
        /// </summary>
        public virtual int Count => count;
        protected int count = 0;

        protected FruitStoredData fruitStoredData = new FruitStoredData();

        public void Init(FruitStoredData fruitStoredData)
        {
            this.fruitStoredData = fruitStoredData.Clone();
            onDataInited();
        }

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