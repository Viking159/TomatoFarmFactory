namespace Features.Store
{
    using System;
    using UnityEngine;
    using UnityEngine.Purchasing;
    using UnityEngine.Purchasing.Extension;

    public sealed class UIAPStore : MonoBehaviour, IDetailedStoreListener
    {
        public event Action onInit = delegate { };
        public event Action<string> onPurchaseSuccess = delegate { };
        public event Action<string> onPurchaseFail = delegate { };

        public static UIAPStore Instance { get; private set; } = default;

        public bool IsInited { get; private set; } = false;

        [SerializeField]
        private string[] _productIDs;

        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            InitializePurchasing();
        }

        public void InitializePurchasing()
        {
            if (IsInitialized()) return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (string productID in _productIDs)
            {
                builder.AddProduct(productID, ProductType.Consumable);
            }
            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized() => m_StoreController != null && m_StoreExtensionProvider != null;

        #region STORE METHODS
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;

            foreach (string productID in _productIDs)
            {
                Product product = m_StoreController.products.WithID(productID);
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log($"Product: {product.metadata.localizedTitle} - {product.metadata.localizedPriceString}");
                }
            }
            IsInited = true;
            onInit();
        }

        public void OnInitializeFailed(InitializationFailureReason error) => OnInitializeFailed(error, string.Empty);

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"Purchase failed for: {error}; {message}");
            IsInited = false;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
            => OnPurchaseFailed(product, new PurchaseFailureDescription(product.definition.id, failureReason, failureReason.ToString()));

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError($"Purchase failed for {product.definition.id}: {failureDescription.message}");
            onPurchaseFail(product.definition.id);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Purchase complete: {purchaseEvent.purchasedProduct.definition.id}");
            onPurchaseSuccess(purchaseEvent.purchasedProduct.definition.id);
            return PurchaseProcessingResult.Complete;
        }
        #endregion

        public void BuyProduct(string productID)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productID);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log($"Purchasing: {product.metadata.localizedTitle} for {product.metadata.localizedPriceString}");
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.LogError($"Product {productID} not available for purchase");
                }
            }
            else
            {
                Debug.LogError("IAP not initialized");
            }
        }

        public Product GetProduct(string productID)
            => IsInitialized() ? m_StoreController.products.WithID(productID) : null;

        public string GetProductPrice(string productID)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productID);
                if (product != null)
                {
                    return product.metadata.localizedPriceString;
                }
            }
            return string.Empty;
        }
    }
}