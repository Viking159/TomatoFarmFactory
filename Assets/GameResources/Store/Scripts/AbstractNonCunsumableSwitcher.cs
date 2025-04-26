namespace Features.Store
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class AbstractNonCunsumableSwitcher : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> enableObjects = new List<GameObject>();
        [SerializeField]
        protected List<GameObject> disableObjects = new List<GameObject>();
        [SerializeField]
        protected string productId = "no_ads";

        protected abstract bool SwitchCondition();

        protected virtual void OnEnable()
        {
            SetView(SwitchCondition());
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