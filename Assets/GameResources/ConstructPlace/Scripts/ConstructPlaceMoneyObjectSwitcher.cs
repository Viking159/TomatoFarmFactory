namespace Features.ConstructPlace
{
    using UnityEngine;
    using Features.Shop;

    /// <summary>
    /// Object switcher devepnding on construction price and money count
    /// </summary>
    [RequireComponent(typeof(ConstructPlaceController))]
    public class ConstructPlaceMoneyObjectSwitcher : AbstractMoneyObjectSwitcher
    {
        protected ConstructPlaceController constructPlaceController = default;

        protected virtual void Awake() => constructPlaceController = GetComponent<ConstructPlaceController>();

        protected override void SetView() => SwitchObjects(constructPlaceController.ConstructPlaceData.Price);
    }
}