namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Update level price text
    /// </summary>
    public class UpdateLevelText : MaskedTextView
    {
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            data.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.UpdateLevelPrice);

        protected virtual void OnDisable()
            => data.onDataChange -= SetText;
    }
}