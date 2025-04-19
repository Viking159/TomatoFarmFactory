namespace Features.Fabric.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using Features.Fabric.Data;
    using Features.InfoBox;
    using System;
    using UnityEngine;

    /// <summary>
    /// Fabric consume speed info text
    /// </summary>
    public class InfoFabricConsumeSpeedText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected FabricData fabricData = default;
        [SerializeField]
        protected int digitsCount = 2;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(baseCreatorInfoBox.SpawnerData == null ? 0
                : Math.Round
                (
                    GlobalData.SPEED_CONVERT_RATIO
                    / fabricData.GetConsumeSpeed(baseCreatorInfoBox.SpawnerData.Rang)
                    / GlobalData.FABRIC_HAND_ANIMATION_SPEED_CONVERT_RATIO,
                    digitsCount
                ));

        protected virtual void OnDisable() => baseCreatorInfoBox.onDataChange -= SetText;
    }
}