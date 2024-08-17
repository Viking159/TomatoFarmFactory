namespace Features.ConstructPlace
{
    using Features.Shop.Data;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Construct place collider controller
    /// </summary>
    public class ConstructPlacePointerCollider : MonoBehaviour
    {
        [SerializeField]
        protected ConstructPlaceController constructPlaceController = default;
        [SerializeField]
        protected MoneyData moneyData = default;

        protected virtual void OnMouseUp()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ConstructPlace();
            }
        }

        protected virtual void ConstructPlace()
        {
            moneyData.SetCoins(moneyData.Coins - constructPlaceController.ConstructPlaceData.Price);
            constructPlaceController.ConstructPlace();
        }
    }
}