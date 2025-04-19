namespace Features.Shop.Data
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Money data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(MoneyData), menuName = "Features/Data/Shop/" + nameof(MoneyData))]
    public class MoneyData : ScriptableObject
    {
        /// <summary>
        /// Money count change event
        /// </summary>
        public event Action onMoneyCountChange = delegate { };

        /// <summary>
        /// Coins count
        /// </summary>
        public virtual int Coins => coins;
        [SerializeField]
        protected int coins = default;

        /// <summary>
        /// Gems count
        /// </summary>
        public virtual int Gems => gems;
        [SerializeField]
        protected int gems = default;

        [SerializeField]
        protected string ppKey = string.Empty;

        protected const int MIN_MONEY_VALUE = 0;

        public virtual void LoadData()
        {
            coins = PlayerPrefs.GetInt(ppKey + nameof(coins), coins);
            gems = PlayerPrefs.GetInt(ppKey + nameof(gems), gems);
        }

        public virtual void SaveData()
        {
            PlayerPrefs.SetInt(ppKey + nameof(coins), coins);
            PlayerPrefs.SetInt(ppKey + nameof(gems), gems);
            PlayerPrefs.Save();
        }

        public virtual void SetCoins(int coinsVal)
        {
            coins = (int)MathF.Max(MIN_MONEY_VALUE, coinsVal);
            onMoneyCountChange();
            SaveData();
        }

        public virtual void SetGems(int gemsVal)
        {
            gems = (int)MathF.Max(MIN_MONEY_VALUE, gemsVal);
            onMoneyCountChange();
            SaveData();
        }
    }
}