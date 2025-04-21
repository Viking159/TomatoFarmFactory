namespace Features.Store
{
    using System.Collections.Generic;
    using UnityEngine;

    public class StoreInitedView : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> initedObjects = new List<GameObject>();
        [SerializeField]
        protected List<GameObject> notInitedObjects = new List<GameObject>();

        protected virtual void OnEnable()
        {
            SetView();
            UIAPStore.Instance.onInit += SetView;
        }

        protected virtual void SetView()
        {
            initedObjects.ForEach(x => x.SetActive(UIAPStore.Instance.IsInited));
            notInitedObjects.ForEach(x => x.SetActive(!UIAPStore.Instance.IsInited));
        }

        protected virtual void OnDisable() => UIAPStore.Instance.onInit -= SetView;
    }
}