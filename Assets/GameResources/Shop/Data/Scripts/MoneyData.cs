namespace Features.Shop.Data
{
    using Extensions.Data;
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
            if (CryptPlayerPrefs.HasKey(ppKey + nameof(coins)))
            {
                coins = CryptPlayerPrefs.GetInt(ppKey + nameof(coins));
            }
            if (CryptPlayerPrefs.HasKey(ppKey + nameof(gems)))
            {
                gems = CryptPlayerPrefs.GetInt(ppKey + nameof(gems));
            }
        }

        public virtual void SaveData()
        {
            CryptPlayerPrefs.SetInt(ppKey + nameof(coins), coins);
            CryptPlayerPrefs.SetInt(ppKey + nameof(gems), gems);
        }

        public virtual void SetCoins(int coinsVal)
        {
            coins = (int)MathF.Max(MIN_MONEY_VALUE, coinsVal);
            onMoneyCountChange();
        }

        public virtual void SetGems(int gemsVal)
        {
            gems = (int)MathF.Max(MIN_MONEY_VALUE, gemsVal);
            onMoneyCountChange();
        }
    }
}