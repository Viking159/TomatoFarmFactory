namespace Features.ConstructPlace
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Construct place collider controller
    /// </summary>
    public class ConstructPlacePointerCollider : MonoBehaviour
    {
        [SerializeField]
        protected ConstructPlaceController constructPlaceController = default;

        protected virtual void OnMouseUp()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                constructPlaceController.ConstructPlace();
            }
        }
    }
}