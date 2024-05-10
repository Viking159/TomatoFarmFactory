namespace Features.Fabric
{
    using Features.Fabric.Data;
    using Features.Product.Data;
    using Features.Product;
    using Features.Spawner;
    using System;
    using UnityEngine;

    /// <summary>
    /// Product fabric
    /// </summary>
    public class ProductFabricController : AbstractPrefabCreator<BaseProduct, FabricData>
    {
        /// <summary>
        /// Change active spawning state event
        /// </summary>
        public event Action onActiveSpwaningStateChange = delegate { };

        /// <summary>
        /// Is spawning in some progress (can be pause state, but can be resumed)
        /// </summary>
        public bool IsActiveSpawning => isActiveSpawning;
        [SerializeField]
        protected bool isActiveSpawning = false;

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

        protected virtual void SetConveyorStateAndActivity()
        {
            SetConveyorState(isSpawning);
            if (isActiveSpawning)
            {
                if (spawnCoroutine == null)
                {
                    StartSpawn();
                }
            }
            else
            {
                StopSpawn();
            }
        }

        protected override void InitData()
            => createdObject.Init(productData);

        protected virtual void NotifyActiveSpwaningStateChange()
            => onActiveSpwaningStateChange();
    }
}