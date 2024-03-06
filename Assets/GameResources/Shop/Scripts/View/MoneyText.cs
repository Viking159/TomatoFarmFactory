namespace Features.Shop
{
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Money text script
    /// </summary>
    public class MoneyText : BaseTextView
    {
        [SerializeField]
        protected MoneyDataContainer moneyDataContainer = default;
        [SerializeField]
        protected string textMask = "Money: {0}";

        protected virtual void OnEnable()
        {
            SetText();
            moneyDataContainer.onMoneyCountChange += SetText;
        }

        protected virtual void SetText()
            => SetView(string.Format(textMask, moneyDataContainer.Coins.ToString()));

        protected virtual void OnDisable()
            => moneyDataContainer.onMoneyCountChange -= SetText;
    }
}