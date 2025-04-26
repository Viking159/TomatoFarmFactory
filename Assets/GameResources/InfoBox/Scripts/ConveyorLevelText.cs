namespace Features.InfoBox
{
    using Features.Extensions.View;
    using UnityEngine;

    public class ConveyorLevelText : MaskedTextView
    {
        [SerializeField]
        protected ConveyorInfoBox infoBoxController = default;

        protected virtual void OnEnable()
        {
            SetText();
            infoBoxController.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(infoBoxController.ConveyorController == null ? string.Empty : infoBoxController.ConveyorController.Level);

        protected virtual void OnDisable()
            => infoBoxController.onDataChange -= SetText;
    }
}