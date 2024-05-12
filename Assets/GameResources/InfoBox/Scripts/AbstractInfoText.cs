namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Abstract ferm info text
    /// </summary>
    public abstract class AbstractInfoText<T> : BaseTextView
        where T: StoreableSO
    {
        [SerializeField]
        protected T data = default;
        [SerializeField]
        protected string mask = "Param: {0}";

        protected virtual void OnEnable()
        {
            SetText();
            data.onDataChange += SetText;
        }

        /// <summary>
        /// Set ferm info text
        /// </summary>
        protected abstract void SetText();

        protected virtual void OnDisable()
            => data.onDataChange -= SetText;
    }
}