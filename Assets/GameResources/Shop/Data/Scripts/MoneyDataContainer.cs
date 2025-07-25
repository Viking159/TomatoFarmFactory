namespace Features.Shop.Data
{
    using Features.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Current money data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(MoneyDataContainer), menuName = "Features/Data/Shop/" + nameof(MoneyDataContainer))]
    public class MoneyDataContainer : InitableData
    {
        /// <summary>
        /// Money count change event
        /// </summary>
        public event Action onMoneyCountChange = delegate { };

        /// <summary>
        /// Coins count
        /// </summary>
        public virtual int Coins => moneyData.Coins;

        /// <summary>
        /// Gems count
        /// </summary>
        public virtual int Gems => moneyData.Gems;

        [SerializeField]
        protected MoneyData moneyData = default;

        public override void Init()
        {
            moneyData.onMoneyCountChange += OnMoneyCountChanged;
            moneyData.LoadData();
        }

        public override void Dispose()
            => moneyData.onMoneyCountChange -= OnMoneyCountChanged;

        protected virtual void OnMoneyCountChanged()
            => NotifyOnMoneyChange();

        protected virtual void NotifyOnMoneyChange() => onMoneyCountChange();
    }
}