namespace Features.ConstructPlace
{
    using Features.ConstructPlace.Data;
    using UnityEngine;

    /// <summary>
    /// Construct place controller
    /// </summary>
    public class ConstructPlaceController : MonoBehaviour
    {
        protected const float MIN_AWAIT = 0.5f;

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

        protected float lastConstructTime = default;
        protected GameObject construction = default;

        protected bool IsValidTime => Time.time - lastConstructTime > MIN_AWAIT;

        /// <summary>
        /// Create consturction
        /// </summary>
        public virtual void ConstructPlace()
        {
            if (IsValidTime)
            {
                lastConstructTime = Time.time;
                CreateConstruction();
                SelfDestroy();
            }
        }

        protected virtual void CreateConstruction()
        {
            construction = Instantiate(constructPlaceData.ConstructPrefab, parentTranform);
            construction.transform.position = constractionPlaceTranform.position;
        }

        protected virtual void SelfDestroy() => Destroy(gameObject);
    }
}