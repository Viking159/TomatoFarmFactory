namespace Features.InfoBox
{
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Rang info text
    /// </summary>
    public class InfoRangText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(baseCreatorInfoBox.SpawnerData.Rang);

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetText;
    }
}