namespace Features.Fruit
{
    using Features.Fruit.Data;
    using System;
    using UnityEngine;
    using Features.Interfaces;

    /// <summary>
    /// Base fruit class
    /// </summary>
    public class BaseFruit : MonoBehaviour, ISaleable, IConsumable
    {
        /// <summary>
        /// Data init event
        /// </summary>
        public event Action onDataInited = delegate { };

        /// <summary>
        /// Fruit level
        /// </summary>
        public virtual int Level { get; protected set; }

        /// <summary>
        /// Is fruit data inited
        /// </summary>
        public virtual bool IsInited { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual int Price { get; protected set; }

        public virtual int Count { get; protected set; }

        /// <summary>
        /// Init fruit data
        /// </summary>
        public virtual void Init(FruitData fruitData)
        {
            Name = fruitData.Name;
            Price = fruitData.Price;
            Count = fruitData.Count;
            Level = fruitData.Level;
            IsInited = true;
            onDataInited();
        }

        public virtual void Sale()
            => Destroy(gameObject);

        public virtual void Consume(IConsumer consumer)
        {
            Debug.Log($"{nameof(BaseFruit)}: Consume");
        }
    }
}