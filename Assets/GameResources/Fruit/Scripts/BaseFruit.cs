namespace Features.Fruit
{
    using Features.Fruit.Data;
    using Features.Interfaces;
    using Features.Spawner;

    /// <summary>
    /// Base fruit class
    /// </summary>
    public class BaseFruit : AbstractInitableObject<FruitData>, ISaleable
    {
        public virtual string Name => data == null ? string.Empty : data.Name;

        public virtual int Price => data == null ? default : data.GetPrice(priceLevel);

        public virtual int Count => data == null ? default : data.GetCount(level);

        public virtual void Sale() => Destroy(gameObject);
    }
}