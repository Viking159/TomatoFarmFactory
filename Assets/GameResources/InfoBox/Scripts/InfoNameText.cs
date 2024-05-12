namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Name info text
    /// </summary>
    public class InfoNameText : MaskedTextView
    {
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            data.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.Name);

        protected virtual void OnDisable()
            => data.onDataChange -= SetText;
    }
}