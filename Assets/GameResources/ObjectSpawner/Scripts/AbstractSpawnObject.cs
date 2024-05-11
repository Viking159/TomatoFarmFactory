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
        /// Object disable event
        /// </summary>
        public event Action onObjectDisable = delegate { };

        /// <summary>
        /// Spawner id
        /// </summary>
        public uint ObjectNumber => objectNumber;
        [SerializeField]
        protected uint objectNumber = 0;

        /// <summary>
        /// Init spawner data
        /// </summary>
        public virtual void SetSpawnNumber(uint nubmer)
            => objectNumber = nubmer;

        protected virtual void NotifyOnDisable()
            => onObjectDisable();

        protected virtual void OnDisable()
            => NotifyOnDisable();
    }
}