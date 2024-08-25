namespace Features.Spawner
{
    using UnityEngine;

    /// <summary>
    /// Abstract object to spawn
    /// </summary>
    public abstract class AbstractSpawnObject : MonoBehaviour
    {
        /// <summary>
        /// Object creator
        /// </summary>
        public AbstractObjectCreator Creator => creator;
        protected AbstractObjectCreator creator = default;

        /// <summary>
        /// Level
        /// </summary>
        public int Level => level;
        protected int level = default;

        /// <summary>
        /// Level of price
        /// </summary>
        public int PriceLevel => priceLevel;
        protected int priceLevel = default;

        /// <summary>
        /// Init spawner data
        /// </summary>
        public virtual void InitCreator(AbstractObjectCreator creator)
            => this.creator = creator;
    }
}