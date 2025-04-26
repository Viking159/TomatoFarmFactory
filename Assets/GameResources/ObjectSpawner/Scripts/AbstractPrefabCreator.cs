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

        protected virtual void Awake() => SetSpawnTime();

        protected virtual void OnEnable() => StartSpawn();

        public override void SetLevel(int level)
        {
            int newLevel = Mathf.Clamp(level, 0, data.MaxLevel);
            if (Level != newLevel)
            {
                base.SetLevel(newLevel);
                SetSpawnTime();
            }
        }

        public override void SetRang(int rang, bool saveAfter = true)
        {
            int newRang = Mathf.Clamp(rang, 0, data.MaxRang);
            if (Rang != newRang)
            {
                base.SetRang(newRang, false);
                SetLevel(0);
            }
        }

        protected override void Spawn()
        {
            NotifySpawnStart();
            spawnedCount++;
            createdObject = Instantiate(prefabObect, spawnPosition);
            createdObject.transform.SetParent(null);
            createdObject.InitCreator(this);
            InitData();
            NotifySpawn();
        }
    }
}