namespace Features.Ferm.InfoBox
{
    using Features.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Ferm spped info text
    /// </summary>
    public class InfoFermSpeedText : AbstractInfoFermText
    {
        [SerializeField]
        protected int digitsCount = 2;

        protected override void SetText()
            => SetView(string.Format(mask, Math.Round(GlobalData.SPEED_CONVERT_RATIO / fermDataContainer.Speed, digitsCount)));
    }
}