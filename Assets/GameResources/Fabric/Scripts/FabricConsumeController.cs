namespace Features.Fabric
{
    using UnityEngine;

    /// <summary>
    /// Fabric consume objects controller
    /// </summary>
    public class FabricConsumeController : MonoBehaviour
    {
        protected const int MIN_CONSUMED_OBJECTS_COUNT = 0;

        [SerializeField]
        protected FabricProductCreatorController fabricProductCreatorController = default;

        protected int cosumedObjectsCount = default;

        /// <summary>
        /// Consume object
        /// </summary>
        public virtual void ConsumeObjects(int count)
        {
            cosumedObjectsCount += count;
            SetSpawnerActivityState();
        }

        protected virtual void Awake() 
            => fabricProductCreatorController.onObjectSpawnStart += ReleaseObjects;

        protected virtual void ReleaseObjects()
        {
            cosumedObjectsCount = Mathf.Max(MIN_CONSUMED_OBJECTS_COUNT, cosumedObjectsCount - fabricProductCreatorController.RequiredFruitsCount);
            SetSpawnerActivityState();
        }

        protected virtual void SetSpawnerActivityState()
            => fabricProductCreatorController.SetActivityState(cosumedObjectsCount >= fabricProductCreatorController.RequiredFruitsCount);

        protected virtual void OnDestroy()
        {
            if (fabricProductCreatorController)
            {
                fabricProductCreatorController.onObjectSpawnStart -= ReleaseObjects;
            }
        }
    }
}