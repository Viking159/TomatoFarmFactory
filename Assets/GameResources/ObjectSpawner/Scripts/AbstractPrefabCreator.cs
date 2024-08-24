namespace Features.Spawner
{
    using Features.Data;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    ///  Abstract prefab spawner
    /// </summary>
    public abstract class AbstractPrefabCreator<PREFAB_TYPE, CREATOR_DATA_TYPE> : AbstractObjectCreator 
        where PREFAB_TYPE: AbstractSpawnObject
        where CREATOR_DATA_TYPE: SpawnerData
    {
        /// <summary>
        /// Creator data
        /// </summary>
        public virtual CREATOR_DATA_TYPE Data => data;
        [SerializeField]
        protected CREATOR_DATA_TYPE data = default;

        [SerializeField]
        protected PREFAB_TYPE prefabObect = default;

        [SerializeField]
        protected bool useRandomStartAwait = true;
        [SerializeField, Min(0)]
        protected float minAwait = 0;
        [SerializeField, Min(0)]
        protected float maxAwait = 1.5f;

        protected PREFAB_TYPE createdObject = default;

        protected uint spawnedCount = 0;

        /// <summary>
        /// Init prefab data
        /// </summary>
        protected abstract void InitData();

        protected virtual void Awake()
        {
            UpdateParams();
            data.onDataChange += UpdateParams;
        }

        protected virtual void OnEnable() => StartSpawn();

        protected virtual void UpdateParams() 
            => SetSpawnTime();

        protected override void SetSpawnTime()
           => spawnTime = GlobalData.SPEED_CONVERT_RATIO / data.Speed;

        protected override void Spawn()
        {
            NotifySpawnStart();
            spawnedCount++;
            createdObject = Instantiate(prefabObect, spawnPosition);
            createdObject.transform.SetParent(null);
            createdObject.SetCreator(this);
            InitData();
            NotifySpawn();
        }

        protected virtual void OnDestroy()
            => data.onDataChange -= UpdateParams;
    }
}