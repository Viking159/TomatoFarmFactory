namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using Features.Ferm.Data;
    using UnityEngine;

    /// <summary>
    /// Ferm name info text
    /// </summary>
    public class InfoFermNameText : AbstractInfoFermText
    {
        protected override void SetText()
            => SetView(string.Format(mask, fermDataContainer.Name));
    }
}