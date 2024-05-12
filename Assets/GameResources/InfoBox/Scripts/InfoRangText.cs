namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Rang info text
    /// </summary>
    public class InfoRangText : MaskedTextView
    {
        [SerializeField]
        protected DoubleStoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            data.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.Rang);

        protected virtual void OnDisable()
            => data.onDataChange -= SetText;
    }
}