namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Update rang button
    /// </summary>
    public class UpdateRangButton : AbstractButtonView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected DoubleStoreableSO data = default;

        protected override void OnButtonClick()
        {
            if (!CheckConditions())
            {
                return;
            }
            baseCreatorInfoBox.Creator.SetRang(baseCreatorInfoBox.SpawnerData.Rang + 1);
        }

        protected virtual bool CheckConditions()
            => baseCreatorInfoBox.SpawnerData != null 
            && baseCreatorInfoBox.SpawnerData.Rang < data.MaxRang;
    }
}