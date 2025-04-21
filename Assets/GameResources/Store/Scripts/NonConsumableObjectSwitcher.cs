namespace Features.Store
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Purchasing;

    public class NonConsumableObjectSwitcher : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> enableObjects = new List<GameObject>();
        [SerializeField]
        protected List<GameObject> disableObjects = new List<GameObject>();
        [SerializeField]
        protected string productId = "no_ads";


        protected virtual void OnEnable()
        {
            Product product = UIAPStore.Instance.GetProduct(productId);
            Debug.Log(product);
            if (product != null )
            {
                Debug.Log(product.availableToPurchase);
                Debug.Log(product.hasReceipt);
            }            
            SetView(product != null && product.availableToPurchase && !product.hasReceipt);
            UIAPStore.Instance.onPurchaseSuccess += HandlePurchace;
        }

        protected virtual void SetView(bool isActive)
        {
            enableObjects.ForEach(x => x.SetActive(isActive));
            disableObjects.ForEach(x => x.SetActive(!isActive));
        }
        protected virtual void HandlePurchace(string id)
        {
            if (id == productId)
            {
                SetView(false);
            }
        }

        protected virtual void OnDisable() => UIAPStore.Instance.onPurchaseSuccess -= HandlePurchace;
    }
}