namespace Features.Fabric.InfoBox
{
    using Features.Data;
    using Features.Fabric.Data;
    using Features.InfoBox;
    using System;
    using UnityEngine;

    /// <summary>
    /// Fabric consume speed info text
    /// </summary>
    public class InfoFabricConsumeSpeedText : AbstractInfoText<FabricData>
    {
        [SerializeField]
        protected int digitsCount = 2;

        protected override void SetText()
            => SetView(string.Format(mask,
                Math.Round
                (
                    GlobalData.SPEED_CONVERT_RATIO 
                    / data.ConsumeSpeed 
                    / GlobalData.FABRIC_HAND_ANIMATION_SPEED_CONVERT_RATIO, 
                    digitsCount
                )));
    }
}