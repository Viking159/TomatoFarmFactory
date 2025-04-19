namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    public class ConveyorInfoBoxUpdateLevelText : MaskedTextView
    {
        [SerializeField]
        protected ConveyorInfoBox infoBox = default;
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            infoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(infoBox.ConveyorController == null ? 0 : data.GetUpdateLevelPrice(infoBox.ConveyorController.Level));

        protected virtual void OnDisable()
            => infoBox.onDataChange -= SetText;
    }
}