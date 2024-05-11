namespace Features.Fabric
{
    using DG.Tweening;
    using Features.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    /// <summary>
    /// Consume animated hand
    /// </summary>
    public class ConsumeHand : MonoBehaviour
    {
        [SerializeField]
        protected FabricProductCreatorController fabricProductCreatorController = default;
        [SerializeField]
        protected FabricFruitsConsumer fabricFruitsConsumer = default;
        [SerializeField]
        protected List<Transform> pathOutPoints = new List<Transform>();
        [SerializeField]
        protected List<Transform> pathInPoints = new List<Transform>();

        protected Tween pathTween = default;
        protected Coroutine animationCoroutine = default;

        protected Vector3[] pathOutVectors = default;
        protected Vector3[] pathInVectors = default;

        protected float animationDuration = default;

        protected virtual void Awake()
            => InitPositions();

        protected virtual void OnEnable()
        {
            SetAnimationSpeed();
            fabricProductCreatorController.Data.onDataChange += SetAnimationSpeed;
            fabricFruitsConsumer.onConsume += AnimatedConsume;
        }

        protected virtual void InitPositions()
        {
            pathOutVectors = pathOutPoints.Select(point => point.position).ToArray();
            pathInVectors = pathInPoints.Select(point => point.position).ToArray();
        }

        protected virtual void SetAnimationSpeed()
            => animationDuration = GlobalData.SPEED_CONVERT_RATIO
            / fabricProductCreatorController.Data.ConsumeSpeed 
            / GlobalData.FABRIC_HAND_ANIMATION_SPEED_CONVERT_RATIO;

        protected virtual void AnimatedConsume()
        {
            StopAnimation();
            animationCoroutine = StartCoroutine(StartAnimation());
        }

        protected virtual IEnumerator StartAnimation()
        {
            AnimatePath(pathOutVectors);
            yield return new WaitForSeconds(animationDuration);
            fabricFruitsConsumer.ConsumableObject.transform.SetParent(transform);
            AnimatePath(pathInVectors);
            yield return new WaitForSeconds(animationDuration);
            Destroy(fabricFruitsConsumer.ConsumableObject);
        }

        protected virtual void AnimatePath(Vector3[] points)
        {
            if (pathTween != null)
            {
                pathTween.Kill();
            }
            pathTween = transform.DOPath
            (
                points,
                animationDuration,
                PathType.Linear
            );
        }

        protected virtual void StopAnimation()
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            if (pathTween != null)
            {
                pathTween.Kill();
            }
        }

        protected virtual void OnDisable()
        {
            fabricFruitsConsumer.onConsume -= AnimatedConsume;
            fabricProductCreatorController.Data.onDataChange -= SetAnimationSpeed;
            StopAnimation();
        }
    }
}

