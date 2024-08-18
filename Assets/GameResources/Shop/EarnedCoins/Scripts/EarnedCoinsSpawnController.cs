namespace Features.Shop
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    /// <summary>
    /// Earned coins spawner
    /// </summary>
    public class EarnedCoinsSpawnController : MonoBehaviour
    {
        protected const int MAX_COINS_OBJECT_COUNT = 25;

        [SerializeField]
        protected Shop shop = default;
        [SerializeField]
        protected EarnedCoinsController earnedCoinsPrefab = default;

        protected List<EarnedCoinsController> earnedCoinsControllers = new List<EarnedCoinsController>();
        protected EarnedCoinsController currentCoinsController = default;

        protected virtual void OnEnable() => shop.onSale += ShowCoins;

        public virtual void ShowCoins(uint coinsCount)
        {
            currentCoinsController = earnedCoinsControllers.FirstOrDefault(el => !el.gameObject.activeInHierarchy);
            if (currentCoinsController == null)
            {
                SetCoinsController();
            }
            currentCoinsController.Init(coinsCount);
            currentCoinsController.gameObject.SetActive(true);
        }

        protected virtual void SetCoinsController()
        {
            if (earnedCoinsControllers.Count >= MAX_COINS_OBJECT_COUNT)
            {
                currentCoinsController = earnedCoinsControllers[UnityEngine.Random.Range(0, earnedCoinsControllers.Count)];
                currentCoinsController.gameObject.SetActive(false);
            }
            else
            {
                SpawnCoins();
            }
        }

        protected virtual void SpawnCoins()
        {
            currentCoinsController = Instantiate(earnedCoinsPrefab, transform);
            earnedCoinsControllers.Add(currentCoinsController);
        }

        protected virtual void OnDisable() => shop.onSale -= ShowCoins;
    }
}