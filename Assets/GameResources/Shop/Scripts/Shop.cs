namespace Features.Shop
{
    using Features.Interfaces;
    using Features.Shop.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Shop controller
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Shop : MonoBehaviour
    {
        /// <summary>
        /// Sale event
        /// </summary>
        public event Action<uint> onSale = delegate { };

        [SerializeField]
        protected MoneyData moneyData = default;

        protected ISaleable saleable = default;
        protected uint price = 0;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            saleable = collision.GetComponent<ISaleable>();
            if (saleable != null)
            {
                Sale();
            }
        }

        protected virtual void Sale()
        {
            price = (uint)(saleable.Price * saleable.Count);
            moneyData.SetCoins(moneyData.Coins + (int)price);
            saleable.Sale();
            NotifyOnSale(price);
        }

        protected virtual void NotifyOnSale(uint price) => onSale(price);
    }
}