namespace Features.Conveyor
{
    using UnityEngine;
    using DG.Tweening;
    using Features.Data;

    /// <summary>
    /// Object on coveyor velocity
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class ConveyorRider : MonoBehaviour
    {
        /// <summary>
        /// Is ride paused
        /// </summary>
        public virtual bool IsPaused { get; protected set; } = false;

        protected ConveyorElement currentConveyorElement = default;
        protected Tween pathTween = default;
        protected Rigidbody2D rb;

        protected virtual void Awake()
           => rb = GetComponent<Rigidbody2D>();

        protected virtual void OnEnable()
            => ResumeRiding();

        /// <summary>
        /// Resume riding
        /// </summary>
        public virtual void ResumeRiding()
        {
            if (pathTween != null && IsPaused)
            {
                IsPaused = false;
                pathTween.Play();
            }
        }

        /// <summary>
        /// Pause riding
        /// </summary>
        public virtual void PauseRiding()
        {
            if (pathTween != null && !IsPaused)
            {
                pathTween.Pause();
                IsPaused = true;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ConveyorElement conveyorElement))
            {
                SetConveyorElement(conveyorElement);
            }
        }

        protected virtual void Ride()
        {
            KillRider();
            if (currentConveyorElement != null && currentConveyorElement.Speed > 0)
            {
                pathTween = rb.DOPath
                (
                    currentConveyorElement.GetPath(transform.position),
                    GetDuration(currentConveyorElement.Speed),
                    PathType.Linear
                );
                if (IsPaused)
                {
                    PauseRiding();
                }
            }
        }

        protected virtual void KillRider()
        {
            if (pathTween != null)
            {
                pathTween.Kill();
            }
        }

        protected virtual float GetDuration(float speed)
            => GlobalData.SPEED_CONVERT_RATIO / speed;

        protected virtual void SetConveyorElement(ConveyorElement conveyorElement)
        {
            ResetConveyorElement();
            currentConveyorElement = conveyorElement;
            Ride();
            currentConveyorElement.onSpeedValueChange += Ride;
        }

        protected virtual void ResetConveyorElement()
        {
            KillRider();
            if (currentConveyorElement != null)
            {
                currentConveyorElement.onSpeedValueChange -= Ride;
            }
        }

        protected virtual void OnDisable()
            => PauseRiding();

        protected virtual void OnDestroy()
            => ResetConveyorElement();
    }
}