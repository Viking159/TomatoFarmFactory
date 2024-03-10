namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Close info box button
    /// </summary>
    public class CloseInfoBoxButton : AbstractButtonView
    {
        [SerializeField]
        protected InfoBox infoBox = default;

        protected override void OnButtonClick()
        {
            infoBox.CloseBox();
        }
    }
}