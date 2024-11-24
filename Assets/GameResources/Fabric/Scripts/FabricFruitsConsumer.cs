namespace Features.Fabric
{
    using Features.Conveyor;
    using Features.Data;
    using Features.Fruit;
    using Features.Interfaces;
    using System;
    using System.Collections;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// Fruits consumer for fabric
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class FabricFruitsConsumer : MonoBehaviour, IConsumer
    {
        protected const float ENABLE_COLLIDER_AWAIT_TIME = 0.5f;

        /// <summary>
        /// Consume fruit event
        /// </summary>
        public event Action onConsume = delegate { };

        /// <summary>
        /// Consume data change event
        /// </summary>
        public event Action onConsumerDataChange = delegate { };

        public string Name => fabricProductCreatorController.Data.Name;

        /// <summary>
        /// Consumed object
        /// </summary>
        public GameObject ConsumableObject { get; protected set; } = default;

        /// <summary>
        /// Time of consuming
        /// </summary>
        public float ConsumeTime => consumeTime;
        protected float consumeTime = default;

        [SerializeField]
        protected FabricConsumeController fabricConsumeController = default;
        [SerializeField]
        protected FabricProductCreatorController fabricProductCreatorController = default;

        protected IConsumable consumableFruit = default;
        protected BaseFruit fruit = default;
        protected Collider2D consumbaleCollider = default;
        protected Coroutine colliderEnableCoroutine = default;
        protected CancellationTokenSource cancellationTokenSource = default;

        protected int consumeCount = 0;
        protected float lastConsumeTime = -100;

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
            consumbaleCollider = GetComponent<Collider2D>();
            SetConsumeAwaitTime();
            fabricProductCreatorController.onDataChange += SetConsumeAwaitTime;
        }

        protected virtual void OnEnable()
        {
            EnableCollider();
            ConveyorController.AddLineAddingStartListener(DisableCollider);
            ConveyorController.AddLineAddingEndListener(EnableCollider);
        }

        protected virtual void EnableCollider() => colliderEnableCoroutine = StartCoroutine(EnableColliderWithAwait());

        protected virtual IEnumerator EnableColliderWithAwait()
        {
            yield return new WaitForSeconds(ENABLE_COLLIDER_AWAIT_TIME);
            consumbaleCollider.enabled = true;
            NotifyOnCosumerDataChange();
        }

        protected virtual void SetConsumeAwaitTime()
        {
            float newconsumeTime = GlobalData.SPEED_CONVERT_RATIO
                       / fabricProductCreatorController.ConsumeSpeed;
            if (consumeTime != newconsumeTime)
            {
                consumeTime = newconsumeTime;
                NotifyOnCosumerDataChange();
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (CheckLastConsumeTime() && collision.TryGetComponent(out consumableFruit) 
                && cancellationTokenSource == null)
            {
                ConsumeObject();
            }
        }

        protected async virtual void ConsumeObject()
        {
            cancellationTokenSource = new CancellationTokenSource();
            consumeCount = consumableFruit.Count;
            Consume(consumableFruit);
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(consumeTime), cancellationTokenSource.Token);
                lastConsumeTime = Time.time;
                fabricConsumeController.ConsumeObjects(consumeCount);
            }
            catch (Exception ex)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    Debug.Log($"{nameof(FabricFruitsConsumer)}: cancel requested");
                }
                else
                {
                    Debug.LogError($"{nameof(FabricFruitsConsumer)}: Ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
            cancellationTokenSource = null;
        }

        protected virtual bool CheckLastConsumeTime() => Time.time - lastConsumeTime >= consumeTime;

        protected virtual void DisableCollider()
        {
            if (colliderEnableCoroutine != null)
            {
                StopCoroutine(colliderEnableCoroutine);
            }
            consumbaleCollider.enabled = false;
            NotifyOnCosumerDataChange();
        }

        protected virtual void NotifyOnConsume() => onConsume();

        protected virtual void NotifyOnCosumerDataChange() => onConsumerDataChange();

        protected virtual void OnDsiable()
        {
            ConveyorController.RemoveLineAddingStartListener(DisableCollider);
            ConveyorController.RemoveLineAddingEndListener(EnableCollider);
            DisableCollider();
        }

        protected virtual void OnDestroy()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
            fabricProductCreatorController.onDataChange -= SetConsumeAwaitTime;
        }
    }
}