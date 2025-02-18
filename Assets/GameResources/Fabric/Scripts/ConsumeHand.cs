namespace Features.Fabric
{
    using UnityEngine;

    /// <summary>
    /// Consume animated hand
    /// </summary>
    public class ConsumeHand : MonoBehaviour
    {
        [SerializeField]
        protected FabricFruitsConsumer fabricFruitsConsumer = default;
        [SerializeField]
        protected FabricAnimationEvents fabricAnimationEvents = default;
        [SerializeField]
        protected Animator animator = default;
        [SerializeField]
        protected string consumeTrigger = "ConsumeTrigger";
        [SerializeField]
        protected Vector3 consumablePosition = Vector3.zero;

        protected virtual void OnEnable()
        {
            SetAnimationTime();
            fabricFruitsConsumer.onConsumerDataChange += SetAnimationTime;
            fabricFruitsConsumer.onConsume += AnimatedConsume;
            fabricAnimationEvents.onHandDown += HandDownHandler;
            fabricAnimationEvents.onConsumeEnd += ConsumeEndHandler;
        }

        protected virtual void SetAnimationTime()
        {
            animator.speed = 1 / fabricFruitsConsumer.ConsumeTime;
        }

        protected virtual void AnimatedConsume() => animator.SetTrigger(consumeTrigger);

        protected virtual void HandDownHandler()
        {
            if (fabricFruitsConsumer.ConsumableObject != null)
            {
                fabricFruitsConsumer.ConsumableObject.transform.SetParent(transform);
                fabricFruitsConsumer.ConsumableObject.transform.localPosition = consumablePosition;
            }
        }

        protected virtual void ConsumeEndHandler()
        {
            if (fabricFruitsConsumer.ConsumableObject != null)
            {
                Destroy(fabricFruitsConsumer.ConsumableObject);
            } 
        }

        protected virtual void OnDisable()
        {
            fabricFruitsConsumer.onConsume -= AnimatedConsume;
            fabricFruitsConsumer.onConsumerDataChange -= SetAnimationTime;
            fabricAnimationEvents.onHandDown -= HandDownHandler;
            fabricAnimationEvents.onConsumeEnd -= ConsumeEndHandler;
        }
    }
}

