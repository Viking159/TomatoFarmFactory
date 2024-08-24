namespace Features.Spawner
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Abstract object to spawn with spawn number
    /// </summary>
    public abstract class AbstractSpawnObject : MonoBehaviour
    {
        /// <summary>
        /// Object creator
        /// </summary>
        public AbstractObjectCreator Creator => creator;
        protected AbstractObjectCreator creator = default;

        /// <summary>
        /// Init spawner data
        /// </summary>
        public virtual void SetCreator(AbstractObjectCreator creator)
            => this.creator = creator;
    }
}