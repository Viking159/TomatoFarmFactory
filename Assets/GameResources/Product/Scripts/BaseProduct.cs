namespace Features.Product
{
    using Features.Interfaces;
    using Features.Product.Data;
    using Features.Spawner;

    /// <summary>
    /// Base product class
    /// </summary>
    public class BaseProduct : AbstractInitableObject<ProductData>, ISaleable
    {
        public virtual string Name => data == null ? string.Empty : data.Name;

        public virtual int Price => data == null ? default : data.GetPrice(priceLevel);

        public virtual int Count => data == null ? default : data.GetFruitsCount(level);

        public virtual void Sale() => Destroy(gameObject);
    }
}