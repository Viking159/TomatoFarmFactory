namespace Features.ConstructPlace
{
    using Features.ConstructPlace.Data;
    using UnityEngine;

    /// <summary>
    /// Construct place controller
    /// </summary>
    public class ConstructPlaceController : MonoBehaviour
    {
        [SerializeField]
        protected Transform parentTranform = default;
        [SerializeField]
        protected Transform constractionPlaceTranform = default;
        [SerializeField]
        protected ConstructPlaceData constructPlaceData = default;

        protected GameObject construction = default;

        /// <summary>
        /// Create consturction and destroy controller
        /// </summary>
        public virtual void ConstructPlace()
        {
            construction = Instantiate(constructPlaceData.ConstructPrefab, parentTranform);
            construction.transform.position = constractionPlaceTranform.position;
            Destroy(gameObject);
        }
    }
}