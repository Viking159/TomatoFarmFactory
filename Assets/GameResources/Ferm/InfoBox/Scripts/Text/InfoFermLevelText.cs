namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using Features.Ferm.Data;
    using UnityEngine;

    /// <summary>
    /// Ferm level info text
    /// </summary>
    public class InfoFermLevelText : AbstractInfoFermText
    {
        protected override void SetText()
            => SetView(string.Format(mask, fermDataContainer.Level));
    }
}