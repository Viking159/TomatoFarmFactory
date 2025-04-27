namespace Features.InfoBox
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
            if (infoBox == null)
            {
                Debug.LogError($"{nameof(CloseInfoBoxButton)}: infobox is null");
                infoBox = GetComponentInParent<InfoBox>();
            }
            infoBox.CloseBox();
        }
    }
}