namespace Features.ConstructPlace.Data
{
    using UnityEngine;

    /// <summary>
    /// Construct place data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ConstructPlaceData), menuName = "Features/ConstructPlace/Data/" + nameof(ConstructPlaceData))]
    public class ConstructPlaceData : ScriptableObject
    {
        /// <summary>
        /// Place short name
        /// </summary>
        public virtual string PlaceName => placeName;
        [SerializeField]
        protected string placeName = string.Empty;

        /// <summary>
        /// Place discription
        /// </summary>
        public virtual string Discription => discription;
        [SerializeField, Multiline]
        protected string discription = string.Empty;

        /// <summary>
        /// Image preview of construction
        /// </summary>
        public virtual Sprite Image => image;
        [SerializeField]
        protected Sprite image = default;

        /// <summary>
        /// Construction prefab
        /// </summary>
        public virtual GameObject ConstructPrefab => constructPrefab;
        [SerializeField]
        protected GameObject constructPrefab = default;

        /// <summary>
        /// Construction price
        /// </summary>
        public virtual float Price => price;
        [SerializeField]
        protected float price = 100;
    }
}