namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using Features.Ferm.Data;
    using UnityEngine;

    /// <summary>
    /// Abstract ferm info text
    /// </summary>
    public abstract class AbstractInfoFermText : BaseTextView
    {
        [SerializeField]
        protected FermDataContainer fermDataContainer = default;
        [SerializeField]
        protected string mask = "Param: {0}";

        protected virtual void OnEnable()
        {
            SetText();
            fermDataContainer.onDataChange += SetText;
        }

        /// <summary>
        /// Set ferm info text
        /// </summary>
        protected abstract void SetText();

        protected virtual void OnDisable()
            => fermDataContainer.onDataChange -= SetText;
    }
}