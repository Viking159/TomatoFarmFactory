namespace Features.Fabric.InfoBox
{
    using Features.Conveyor.Data;
    using Features.Extensions.View;
    using Features.InfoBox;
    using System;
    using UnityEngine;

    public class ConveyorInfoBoxSpeedText : MaskedTextView
    {
        [SerializeField]
        protected ConveyorInfoBox infoBox = default;
        [SerializeField]
        protected ConveyorData conveyorData = default;
        [SerializeField]
        protected int digitsCount = 2;

        protected virtual void OnEnable()
        {
            SetText();
            infoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(infoBox.ConveyorController == null ? 0
                : Math.Round
                (
                    conveyorData.GetSpeed(infoBox.ConveyorController.Level),
                    digitsCount
                ));

        protected virtual void OnDisable() => infoBox.onDataChange -= SetText;
    }
}