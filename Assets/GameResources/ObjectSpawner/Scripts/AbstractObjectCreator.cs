namespace Features.Spawner
{
    using Features.Data;
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Abstract object spawner
    /// </summary>
    public abstract class AbstractObjectCreator : MonoBehaviour
    {
        protected const int MIN_PROGRESS_VALUE = 0;
        protected const int MAX_PROGRESS_VALUE = 1;

        /// <summary>
        /// Data change event
        /// </summary>
        public event Action onDataChange = delegate { };

        /// <summary>
        /// Spawner state change event
        /// </summary>
        public event Action onSpawnStateChange = delegate { };

        /// <summary>
        /// Object spawn event
        /// </summary>
        public event Action onObjectSpawn = delegate { };

        /// <summary>
        /// Start object spawn event
        /// </summary>
        public event Action onObjectSpawnStart = delegate { };

        /// <summary>
        /// Progress value change event
        /// </summary>
        public event Action onProgressValueChange = delegate { };

        /// <summary>
        /// Object spawn speed
        /// </summary>
        public abstract float Speed { get; }

        /// <summary>
        /// Level
        /// </summary>
        public virtual int Level => spawnerData.Level;

        /// <summary>
        /// Rang
        /// </summary>
        public virtual int Rang => spawnerData.Rang;

        /// <summary>
        /// Object spawn progress (0..1)
        /// </summary>
        public virtual float Progress => progress;
        protected float progress = default;

        /// <summary>
        /// Creator index in creators line
        /// </summary>
        public int Index => spawnerData.Index;

        /// <summary>
        /// Data
        /// </summary>
        public SaveSystem.SpawnerData SpawnerData => spawnerData;
        protected SaveSystem.SpawnerData spawnerData = new SaveSystem.SpawnerData();

        public virtual bool IsSpawning => isSpawning;
        [SerializeField]
        protected bool isSpawning = true;

        /// <summary>
        /// Spawn time
        /// </summary>
        public float SpawnTime => spawnTime;
        protected float spawnTime = default;


        [SerializeField]
        protected Transform spawnPosition = default;

        
        protected Coroutine spawnCoroutine = default;
        protected Coroutine countTimeCoroutine = default;


        protected float curTime = default;

        /// <summary>
        /// Init creator data
        /// </summary>
        public virtual void InitData(SaveSystem.SpawnerData spawnerData)
        {
            this.spawnerData = spawnerData;
            SetSpawnTime();
            NotifyOnDataChange();
        }

        /// <summary>
        /// Set level
        /// </summary>
        public virtual void SetLevel(int level)
        {
            spawnerData.Level = level;
            NotifyOnDataChange();
        }

        /// <summary>
        /// Set rang
        /// </summary>
        public virtual void SetRang(int rang)
        {
            spawnerData.Rang = rang;
            NotifyOnDataChange();
        }

        /// <summary>
        /// Set conveyor progress state
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
        /// Spawn objects
        /// </summary>
        protected abstract void Spawn();

        /// <summary>
        /// Set spawn time
        /// </summary>
        protected virtual void SetSpawnTime() 
            => spawnTime = GlobalData.SPEED_CONVERT_RATIO / Speed;

        protected virtual IEnumerator SpawnObjects()
        {
            while (isActiveAndEnabled)
            {
                if (countTimeCoroutine != null)
                {
                    StopCoroutine(countTimeCoroutine);
                }
                countTimeCoroutine = StartCoroutine(CountTime());
                yield return countTimeCoroutine;
                Spawn();
            }
        }

        protected virtual IEnumerator CountTime()
        {
            SetProgress(MIN_PROGRESS_VALUE);
            curTime = 0;
            while (curTime < spawnTime)
            {
                while (!isSpawning)
                {
                    yield return null;
                }
                SetProgress(curTime / spawnTime);
                curTime += Time.deltaTime;
                yield return null;
            }
            SetProgress(MAX_PROGRESS_VALUE);
        }

        protected virtual void SetProgress(float val)
        {
            float clampedVal = Mathf.Clamp01(val);
            if (progress != clampedVal)
            {
                progress = clampedVal;
                NotfityProgress();
            }
        }

        protected virtual void StartSpawn()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            SetProgress(MIN_PROGRESS_VALUE);
            spawnCoroutine = StartCoroutine(SpawnObjects());
        }

        protected virtual void StopSpawn()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            if (countTimeCoroutine != null)
            {
                StopCoroutine(countTimeCoroutine);
            }
            spawnCoroutine = null;
            SetProgress(MIN_PROGRESS_VALUE);
        }

        protected virtual void NotifySpawnState() => onSpawnStateChange();

        protected virtual void NotifySpawn() => onObjectSpawn();

        protected virtual void NotifySpawnStart() => onObjectSpawnStart();

        protected virtual void NotfityProgress() => onProgressValueChange();

        protected virtual void NotifyOnDataChange() => onDataChange();

        protected virtual void OnDisable() 
            => StopSpawn();
    }
}