namespace Features.Store
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Purchasing;
    using UnityEngine.Purchasing.Extension;

    public sealed class UIAPStore : MonoBehaviour, IDetailedStoreListener
    {
        private const string BOUGHT_PREFS_KEY = "Bought_";


        public event Action onInit = delegate { };
        public event Action<string> onPurchaseSuccess = delegate { };
        public event Action<string> onPurchaseFail = delegate { };

        public static UIAPStore Instance { get; private set; } = default;

        public bool IsInited { get; private set; } = false;

        [SerializeField]
        private string[] _productIDs;

        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;

        private readonly YieldInstruction awaiter = new WaitForSeconds(5);

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
                builder.AddProduct(productID, ProductType.NonConsumable);
            }
            builder.Configure<IGooglePlayConfiguration>().SetObfuscatedAccountId(SystemInfo.deviceUniqueIdentifier);
            builder.Configure<IGooglePlayConfiguration>().SetDeferredPurchaseListener(OnDeferredPurchase);
            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized() => m_StoreController != null && m_StoreExtensionProvider != null;

        #region STORE METHODS
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            Debug.Log("Available products:");
            foreach (Product product in controller.products.all)
            {
                if (product != null)
                {
                    Debug.Log($"id={product.definition.id}; availableToPurchase={product.availableToPurchase}; hasReciept={product.hasReceipt}");
#if UNITY_EDITOR
                    if (IsBought(product.definition.id))
                    {
                        BuyProduct(product.definition.id);
                    }
#endif
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

        private void OnDeferredPurchase(Product product)
        {
            if (product != null)
            {
                Debug.Log($"Deferred purchase complete: {product.definition.id}");
                StartCoroutine(CheckPurchaseStatus(product.definition.id));
            }
        }

        private IEnumerator CheckPurchaseStatus(string productId)
        {
            while (isActiveAndEnabled)
            {
                yield return awaiter;
                Product product = m_StoreController.products.WithID(productId);
                if (product == null)
                {
                    Debug.Log($"No product with id: {productId}");
                    yield break;
                }    
                if (product.hasReceipt)
                {
                    Debug.Log($"Deferred purchase approved for {product.definition.id}");
                    OnPurchase(product.definition.id);
                    yield break;
                }
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Purchase complete: {purchaseEvent.purchasedProduct.definition.id}; hasReciept: {purchaseEvent.purchasedProduct.hasReceipt}; available: {purchaseEvent.purchasedProduct.availableToPurchase}");
            OnPurchase(purchaseEvent.purchasedProduct.definition.id);
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

        public bool IsAvailableToPurchase(string productID)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productID);
                return product.availableToPurchase;
            }
            return false;
        }

        public bool IsBought(string productID)
        {
            if (PlayerPrefs.GetInt(BOUGHT_PREFS_KEY + productID) == 1)
            {
                return true;
            }
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productID);
                return product.hasReceipt;
            }
            return false;
        }


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

        private void OnPurchase(string id)
        {
            PlayerPrefs.SetInt(BOUGHT_PREFS_KEY + id, 1);
            onPurchaseSuccess(id);
        }

#if UNITY_EDITOR
        [ContextMenu("Clear data")]
        private void ClearData()
        {
            foreach (string productId in _productIDs)
            {
                PlayerPrefs.DeleteKey(BOUGHT_PREFS_KEY + productId);
                Debug.Log($"UIAP: {productId} data deleted");
            }
        }
#endif
    }
}