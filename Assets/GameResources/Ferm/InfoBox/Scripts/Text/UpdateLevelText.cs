namespace Features.Ferm.InfoBox
{
    /// <summary>
    /// Update level price text
    /// </summary>
    public class UpdateLevelText : AbstractInfoFermText
    {
        protected override void SetText()
            => SetView(string.Format(mask, fermDataContainer.UpdateLevelPrice));
    }
}