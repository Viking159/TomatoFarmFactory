namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using Features.Ferm.Data;
    using UnityEngine;

    /// <summary>
    /// Ferm rang info text
    /// </summary>
    public class InfoFermRangText : AbstractInfoFermText
    {
        protected override void SetText() 
            => SetView(string.Format(mask, fermDataContainer.Rang));
    }
}