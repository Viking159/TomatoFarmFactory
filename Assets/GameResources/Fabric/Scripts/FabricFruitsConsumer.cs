namespace Features.Fabric
{
    using Features.Data;
    using Features.Fruit;
    using Features.Interfaces;
    using System;
    using UnityEngine;

    /// <summary>
    /// Fruits consumer for fabric
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class FabricFruitsConsumer : MonoBehaviour, IConsumer
    {
        /// <summary>
        /// Consume fruit event
        /// </summary>
        public event Action onConsume = delegate { };

        /// <summary>
        /// Consumed object
        /// </summary>
        public GameObject ConsumableObject { get; protected set; } = default;

        [SerializeField]
        protected FabricConsumeController fabricConsumeController = default;
        [SerializeField]
        protected FabricProductCreatorController fabricProductCreatorController = default;
        [SerializeField]
        protected Transform handInTranform = default;

        protected IConsumable consumableFruit = default;
        protected BaseFruit fruit = default;

        protected float consumeAwaitTime = default;
        protected float lastConsumeTime = default;

        public string Name => fabricProductCreatorController.Data.Name;

        public virtual void Consume(IConsumable consumable)
        {
            if (consumable is MonoBehaviour consumableObject)
            {
                fruit = consumableObject.GetComponentInParent<BaseFruit>();
                if (fruit != null)
                {
                    consumable.Consume(this);
                    ConsumableObject = fruit.gameObject;
                    NotifyOnConsume();
                }
            }
        }

        protected virtual void Awake()
        {
            SetConsumeAwaitTime();
            lastConsumeTime = -consumeAwaitTime;
            fabricProductCreatorController.Data.onDataChange += SetConsumeAwaitTime;
        }

        protected virtual void SetConsumeAwaitTime()
            => consumeAwaitTime = GlobalData.SPEED_CONVERT_RATIO 
            / fabricProductCreatorController.Data.ConsumeSpeed;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (CheckLastConsumeTime() && collision.TryGetComponent(out consumableFruit))
            {
                lastConsumeTime = Time.time;
                fabricConsumeController.ConsumeObjects(consumableFruit.Count);
                Consume(consumableFruit);
            }
        }

        protected virtual bool CheckLastConsumeTime()
            => Time.time - lastConsumeTime >= consumeAwaitTime;

        protected virtual void NotifyOnConsume()
            => onConsume();

        protected virtual void OnDestroy()
            => fabricProductCreatorController.Data.onDataChange -= SetConsumeAwaitTime;
    }
}