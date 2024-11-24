namespace Features.Fabric
{
    using DG.Tweening;
    using Features.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using Features.Conveyor;

    /// <summary>
    /// Consume animated hand
    /// </summary>
    public class ConsumeHand : MonoBehaviour
    {
        [SerializeField]
        protected FabricFruitsConsumer fabricFruitsConsumer = default;
        [SerializeField]
        protected List<Transform> pathOutPoints = new List<Transform>();
        [SerializeField]
        protected List<Transform> pathInPoints = new List<Transform>();

        protected Tween pathHandTween = default;
        protected Tween pathObjectTween = default;
        protected Coroutine animationCoroutine = default;

        protected Vector3[] pathOutVectors = default;
        protected Vector3[] pathInVectors = default;

        protected float animationDuration = default;

        protected virtual void OnEnable()
        {
            OnConsumerDataChanged();
            fabricFruitsConsumer.onConsumerDataChange += OnConsumerDataChanged;
            fabricFruitsConsumer.onConsume += AnimatedConsume;
        }

        protected virtual void OnConsumerDataChanged()
        {
            InitPositions();
            SetAnimationTime();
        }

        protected virtual void SetAnimationTime()
            => animationDuration = fabricFruitsConsumer.ConsumeTime / GlobalData.FABRIC_HAND_ANIMATION_SPEED_CONVERT_RATIO;

        protected virtual void InitPositions()
        {
            pathOutVectors = pathOutPoints.Select(point => point.position).ToArray();
            pathInVectors = pathInPoints.Select(point => point.position).ToArray();
        }

        protected virtual void AnimatedConsume()
        {
            StopAnimations();
            animationCoroutine = StartCoroutine(StartAnimation());
        }

        protected virtual IEnumerator StartAnimation()
        {
            AnimateConsumableObject();
            AnimateHandPath(pathOutVectors);
            yield return new WaitForSeconds(animationDuration);
            fabricFruitsConsumer.ConsumableObject.transform.SetParent(transform);
            AnimateHandPath(pathInVectors);
            yield return new WaitForSeconds(animationDuration);
            Destroy(fabricFruitsConsumer.ConsumableObject);
        }

        protected virtual void AnimateConsumableObject()
        {
            if (pathObjectTween != null)
            {
                pathObjectTween.Kill();
            }
            pathObjectTween = fabricFruitsConsumer.ConsumableObject.transform.DOPath
                (
                    new Vector3[2] { fabricFruitsConsumer.ConsumableObject.transform.position, pathOutVectors.Last() },
                    animationDuration,
                    PathType.Linear
                );
        }

        protected virtual void AnimateHandPath(Vector3[] points)
        {
            if (pathHandTween != null)
            {
                pathHandTween.Kill();
            }
            pathHandTween = transform.DOPath
            (
                points,
                animationDuration,
                PathType.Linear
            );
        }

        protected virtual void StopAnimations()
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            if (pathHandTween != null)
            {
                pathHandTween.Kill();
            }
            if (pathObjectTween != null)
            {
                pathObjectTween.Kill();
            }
        }

        protected virtual void OnDisable()
        {
            fabricFruitsConsumer.onConsume -= OnConsumerDataChanged;
            StopAnimations();
        }
    }
}

