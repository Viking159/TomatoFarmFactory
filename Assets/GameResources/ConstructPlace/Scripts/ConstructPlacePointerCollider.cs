namespace Features.ConstructPlace
{
    using Features.InfoBox;
    using Features.Shop.Data;
    using UnityEngine;

    /// <summary>
    /// Construct place collider controller
    /// </summary>
    public class ConstructPlacePointerCollider : AbstractClickController
    {
        [SerializeField]
        protected ConstructPlaceController constructPlaceController = default;
        [SerializeField]
        protected MoneyData moneyData = default;

        protected override void ClickHandle() => ConstructPlace();

        protected virtual void ConstructPlace()
        {
            moneyData.SetCoins(moneyData.Coins - constructPlaceController.ConstructPlaceData.Price);
            constructPlaceController.ConstructPlace();
        }
    }
}