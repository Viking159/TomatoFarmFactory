namespace Features.InfoBox
{
    /// <summary>
    /// Ferm name info text
    /// </summary>
    public class FermInfoNameText : InfoNameText
    {
        protected override int GetIndex()
        {
            if (IsDataInited())
            {
                if (infoBoxController.Creator.ConveyorLineController.Index > 0)
                {
                    return base.GetIndex() - 1;
                }
            }
            return 1;
        }
    }
}