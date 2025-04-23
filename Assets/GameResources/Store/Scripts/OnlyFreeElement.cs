namespace Features.Store
{
    using UnityEngine;
    using UnityEngine.Purchasing;

    public sealed class OnlyFreeElement : MonoBehaviour
    {
        [SerializeField]
        private string _productId = "no_ads";

        private void OnEnable()
        {
            if (UIAPStore.Instance.IsBought(_productId))
            {
                DestroyObject();
                return;
            }
            UIAPStore.Instance.onPurchaseSuccess += HandlePurchace;
        }

        private void HandlePurchace(string id)
        {
            if (id == _productId)
            {
                DestroyObject();
            }
        }

        private void DestroyObject() => Destroy(gameObject);

        private void OnDisable() => UIAPStore.Instance.onPurchaseSuccess -= HandlePurchace;
    }
}
