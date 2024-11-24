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
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.GetUpdateLevelPrice(baseCreatorInfoBox.SpawnerData.Level));

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetText;
    }
}