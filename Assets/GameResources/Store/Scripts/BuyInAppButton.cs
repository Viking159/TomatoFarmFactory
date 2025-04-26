namespace Features.Store
{
    using Features.Extensions.View;
    using UnityEngine;
    using UnityEngine.Purchasing;

    public class BuyInAppButton : AbstractButtonView
    {
        [SerializeField]
        protected string productId = string.Empty;

        protected override void OnButtonClick()
        {
            Product product = UIAPStore.Instance.GetProduct(productId);
            if (UIAPStore.Instance.IsInited && product != null && product.availableToPurchase)
            {
                UIAPStore.Instance.BuyProduct(productId);
            }
        }
    }
}