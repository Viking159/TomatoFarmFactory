namespace Features.Product
{
    using Features.Interfaces;
    using Features.Product.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Base product class
    /// </summary>
    public class BaseProduct : MonoBehaviour, ISaleable
    {
        /// <summary>
        /// Data init event
        /// </summary>
        public event Action onDataInited = delegate { };

        public virtual string Name => productData == null ? string.Empty : productData.Name;

        public virtual int Price => productData == null ? default : productData.Price;

        public virtual int Count => productData == null ? default : productData.FruitsCount;

        protected ProductData productData = default;

        public virtual void Init(ProductData productData)
        {
            this.productData = productData;
            NotifyDataInit();
        }

        public virtual void Sale()
            => Destroy(gameObject);

        protected virtual void NotifyDataInit()
            => onDataInited();
    }
}