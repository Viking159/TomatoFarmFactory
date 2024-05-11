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

        /// <summary>
        /// Pause riding
        /// </summary>
        public virtual void PauseRiding()
        {
            if (pathTween != null)
            {
                pathTween.Pause();
                IsPaused = true;
            }
        }

        /// <summary>
        /// Resume riding
        /// </summary>
        public virtual void ResumRiding()
        {
            if (pathTween != null)
            {
                IsPaused = false;
                pathTween.Play();
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

        protected virtual void OnDestroy()
            => ResetConveyorElement();
    }
}