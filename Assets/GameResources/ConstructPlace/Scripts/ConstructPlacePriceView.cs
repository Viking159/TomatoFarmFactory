namespace Features.ConstructPlace
{
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Construct place price view
    /// </summary>
    public class ConstructPlacePriceView : MaskedTextView
    {
        [SerializeField]
        protected ConstructPlaceController constructPlaceController = default;

        protected virtual void OnEnable() => SetView(constructPlaceController.ConstructPlaceData.Price);
    }
}