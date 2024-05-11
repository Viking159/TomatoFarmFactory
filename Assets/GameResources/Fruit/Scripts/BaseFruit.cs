namespace Features.Fruit
{
    using Features.Fruit.Data;
    using UnityEngine;
    using Features.Interfaces;
    using Features.Spawner;

    /// <summary>
    /// Base fruit class
    /// </summary>
    public class BaseFruit : AbstractInitableObject<FruitData>, ISaleable
    {
        /// <summary>
        /// Fruit level
        /// </summary>
        public virtual int Level => data == null ? default : data.Level;

        public virtual string Name => data == null ? string.Empty : data.Name;

        public virtual int Price => data == null ? default : data.Price;

        public virtual int Count => data == null ? default : data.Count;

        public virtual void Sale()
            => Destroy(gameObject);
    }
}