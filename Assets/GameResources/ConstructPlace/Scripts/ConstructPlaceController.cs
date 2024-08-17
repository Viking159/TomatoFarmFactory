namespace Features.ConstructPlace
{
    using Features.ConstructPlace.Data;
    using UnityEngine;

    /// <summary>
    /// Construct place controller
    /// </summary>
    public class ConstructPlaceController : MonoBehaviour
    {
        /// <summary>
        /// Construct Place Data container
        /// </summary>
        public ConstructPlaceData ConstructPlaceData => constructPlaceData;
        [SerializeField]
        protected ConstructPlaceData constructPlaceData = default;

        [SerializeField]
        protected Transform parentTranform = default;
        [SerializeField]
        protected Transform constractionPlaceTranform = default;
        
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