namespace Features.Data
{
    using UnityEngine;

    /// <summary>
    /// Storeable SO with sprites data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(StoreableSOWithSprites), menuName = "Features/Data/" + nameof(StoreableSOWithSprites))]
    public class StoreableSOWithSprites : StoreableSO
    {
        /// <summary>
        /// Sprites data
        /// </summary>
        public SpritesData SpritesData => spritesData;
        [SerializeField]
        protected SpritesData spritesData = default;
    }
}