namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Update rang price text
    /// </summary>
    public class UpdateRangText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected DoubleStoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.GetUpdateRangPrice(baseCreatorInfoBox.SpawnerData.Rang));

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetText;
    }
}