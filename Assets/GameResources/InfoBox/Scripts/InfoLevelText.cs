namespace Features.InfoBox
{
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Level info text
    /// </summary>
    public class InfoLevelText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(baseCreatorInfoBox.SpawnerData.Level);

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetText;
    }
}