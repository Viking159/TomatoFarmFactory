namespace Features.Shop
{
    using UnityEngine;
    using DG.Tweening;
    using System;

    /// <summary>
    /// Spawned earned coins controller
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class EarnedCoinsController : MonoBehaviour
    {
        /// <summary>
        /// Earned coins data init event
        /// </summary>
        public event Action onDataInit = delegate { };

        /// <summary>
        /// Coins count
        /// </summary>
        public ulong CoinsCount { get; protected set; } = 0;

        [SerializeField, Min(0)]
        protected float yDelta = 220;
        [SerializeField, Min(0)]
        protected float animationTime = 1.5f;

        protected RectTransform rectTransform = default;
        protected Tween moveTween = default;

        /// <summary>
        /// Init earned coins controller
        /// </summary>
        public virtual void Init(uint coinsCount)
        {
            CoinsCount = coinsCount;
            NotifyOnInit();
        }

        protected virtual void Awake() => rectTransform = GetComponent<RectTransform>();

        protected virtual void OnEnable() => AnimateMove();

        protected virtual void AnimateMove()
        {
            rectTransform.anchoredPosition = Vector2.zero;
            moveTween = rectTransform.DOAnchorPosY(yDelta, animationTime);
            moveTween.onComplete += OnTweenCompleted;
        }

        protected virtual void OnTweenCompleted()
        {
            DestroyTween();
            gameObject.SetActive(false);
        }

        protected virtual void DestroyTween()
        {
            if (moveTween != null)
            {
                moveTween.onComplete -= OnTweenCompleted;
                moveTween.Kill();
            }
        }

        protected virtual void NotifyOnInit() => onDataInit();

        protected virtual void OnDisable() => DestroyTween();
    }
}