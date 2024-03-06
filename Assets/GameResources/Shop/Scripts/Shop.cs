namespace Features.Shop
{
    using Features.Interfaces;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Shop controller
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Shop : MonoBehaviour
    {
        [SerializeField]
        protected MoneyData moneyData = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ISaleable saleable = collision.GetComponent<ISaleable>();
            if (saleable == null)
            {
                return; 
            }
            moneyData.SetCoins(moneyData.Coins + saleable.Price * saleable.Count);
            saleable.Sale();
        }
    }
}