namespace Features.Shop
{
    using Features.Extensions;
    using Features.Shop.Data;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Abstract objects switcher depending on enough money
    /// </summary>
    public abstract class AbstractMoneyObjectSwitcher : BaseObjectSwitcher
    {
        [SerializeField]
        protected MoneyData moneyData = default;

        /// <summary>
        /// Should call SwitchObjects method with requiredCoinsCount in param
        /// </summary>
        protected abstract void SetView();

        protected virtual void OnEnable()
        {
            SetView();
            moneyData.onMoneyCountChange += SetView;
        }

        protected virtual void SwitchObjects(int requiredCoinsCount) 
            => SetObjects(moneyData.Coins >= requiredCoinsCount);

        protected virtual void OnDisable() => moneyData.onMoneyCountChange -= SetView;
    }
}