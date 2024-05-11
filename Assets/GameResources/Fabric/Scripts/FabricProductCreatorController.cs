namespace Features.Fabric
{
    using Features.Fabric.Data;
    using Features.Product.Data;
    using Features.Product;
    using Features.Spawner;
    using System;
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// Product creator
    /// </summary>
    public class FabricProductCreatorController : AbstractPrefabCreator<BaseProduct, FabricData>
    {
        /// <summary>
        /// Change active spawning state event
        /// </summary>
        public event Action onActiveSpwaningStateChange = delegate { };

        /// <summary>
        /// Is spawning in some progress (can be in pause state, but can be resumed)
        /// </summary>
        public bool IsActiveSpawning => isActiveSpawning;
        [SerializeField]
        protected bool isActiveSpawning = false;

        /// <summary>
        /// Required count of fruits
        /// </summary>
        public int RequiredFruitsCount => productData.FruitsCount;

        public override float Speed => data.Speed;

        [SerializeField]
        protected ProductData productData = default;

        /// <summary>
        /// Set spawning activity state
        /// </summary>
        public virtual void SetActivityState(bool isActive)
        {
            if (isActiveSpawning != isActive)
            {
                isActiveSpawning = isActive;
                SetConveyorStateAndActivity();
                NotifyActiveSpwaningStateChange();
            }
        }

        protected override void OnEnable() 
            => SetConveyorStateAndActivity();

        protected override void UpdateParams()
        {
            base.UpdateParams();
            SetProductData();
        }

        protected virtual void SetProductData()
            => productData.SetLevel(data.Rang);

        protected override IEnumerator SpawnObjects()
        {
            while (isActiveAndEnabled && isActiveSpawning)
            {
                yield return CountTime();
                Spawn();
            }
            SetProgress(MIN_PROGRESS_VALUE);
            spawnCoroutine = null;
        }

        protected virtual void SetConveyorStateAndActivity()
        {
            SetConveyorState(isSpawning);
            if (isActiveSpawning && spawnCoroutine == null)
            {
                StartSpawn();
            }
        }

        protected override void InitData()
            => createdObject.InitData(productData);

        protected virtual void NotifyActiveSpwaningStateChange()
            => onActiveSpwaningStateChange();
    }
}