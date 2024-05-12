namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Level info text
    /// </summary>
    public class InfoLevelText : MaskedTextView
    {
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            data.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.Level);

        protected virtual void OnDisable()
            => data.onDataChange -= SetText;
    }
}