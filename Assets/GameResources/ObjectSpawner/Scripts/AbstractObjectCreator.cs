namespace Features.Spawner
{
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Abstract object spawner
    /// </summary>
    public abstract class AbstractObjectCreator : MonoBehaviour
    {
        /// <summary>
        /// Spawner state change event
        /// </summary>
        public event Action onSpawnStateChange = delegate { };

        /// <summary>
        /// Object spawn event
        /// </summary>
        public event Action onObjectSpawn = delegate { };

        /// <summary>
        /// Progress value change event
        /// </summary>
        public event Action onProgressValueChange = delegate { };

        /// <summary>
        /// Object spawn speed
        /// </summary>
        public abstract float Speed { get; }

        /// <summary>
        /// Object spawn progress (0..1)
        /// </summary>
        public virtual float Progress => progress;
        protected float progress = default;

        public virtual bool IsSpawning => isSpawning;
        [SerializeField]
        protected bool isSpawning = true;

        [SerializeField]
        protected Transform spawnPosition = default;
        [SerializeField]
        protected Transform fruitParent = default;

        protected Coroutine spawnCoroutine = default;

        protected const int MAX_PROGRESS_VALUE = 1;

        /// <summary>
        /// Set emergency break value
        /// </summary>
        public virtual void SetConveyorState(bool isSpawning)
        {
            if (this.isSpawning != isSpawning)
            {
                this.isSpawning = isSpawning;
                NotifySpawnState();
            }            
        }

        /// <summary>
        /// Spawn objects coroutine
        /// </summary>
        protected abstract IEnumerator Spawn();

        protected virtual void SetProgress(float val)
        {
            float clampedVal = Mathf.Clamp01(val);
            if (progress != clampedVal)
            {
                progress = clampedVal;
                NotfityProgress();
            }
        }

        protected virtual void NotifySpawnState()
            => onSpawnStateChange();

        protected virtual void NotifySpawn()
            => onObjectSpawn();

        protected virtual void NotfityProgress()
            => onProgressValueChange();
    }
}