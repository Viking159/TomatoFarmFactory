namespace Features.InfoBox
{
    using Features.Extensions.View;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Abstract update object button
    /// </summary>
    public abstract class AbstractUpdateButton<T> : AbstractButtonView
    {
        [SerializeField]
        protected T data = default;
        [SerializeField]
        protected MoneyData moneyData = default;

        protected override void OnButtonClick()
        {
            if (!CheckConditions())
            {
                return;
            }
        }

        protected abstract bool CheckConditions();
    }
}