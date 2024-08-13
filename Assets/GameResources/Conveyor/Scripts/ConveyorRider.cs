namespace Features.Conveyor
{
    using UnityEngine;
    using DG.Tweening;
    using Features.Data;
    using System;

    /// <summary>
    /// Object on coveyor velocity
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class ConveyorRider : MonoBehaviour
    {
        protected const int PREVIOUS_ELEMENT_INDEX = 1;

        /// <summary>
        /// Is ride paused
        /// </summary>
        public virtual bool IsPaused { get; protected set; } = false;

        protected ConveyorElement currentConveyorElement = default;
        protected Tween pathTween = default;
        protected Rigidbody2D rb = default;
        protected Vector2[] path = default;

        /// <summary>
        /// Resume riding
        /// </summary>
        public virtual void ResumeRiding()
        {
            if (pathTween != null && IsPaused)
            {
                IsPaused = false;
                ResumeRide();
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

        /// <summary>
        /// Stop riding
        /// </summary>
        public virtual void KillRider()
        {
            if (pathTween != null)
            {
                pathTween.Kill();
            }
        }

        protected virtual void Awake()
           => rb = GetComponent<Rigidbody2D>();

        protected virtual void OnEnable()
        {
            ResumeRiding();
            ConveyorController.onLineAddStart += OnLineAddStarted;
            ConveyorController.onLineAddEnd += OnLineAddEnded;
        }

        protected virtual void OnLineAddStarted() => PauseRiding();

        protected virtual void OnLineAddEnded() => ResumeRiding();

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
                path = currentConveyorElement.GetPath(transform.position);
                pathTween = rb.DOPath
                (
                    currentConveyorElement.GetPath(transform.position),
                    GetDuration(path, currentConveyorElement.Speed),
                    PathType.Linear
                );
                if (IsPaused)
                {
                    PauseRiding();
                }
            }
        }

        protected virtual void ResumeRide()
        {
            KillRider();
            if (currentConveyorElement != null && currentConveyorElement.Speed > 0)
            {
                path = currentConveyorElement.GetActualPath(transform.position);
                pathTween = rb.DOPath
                (
                    path,
                    GetDuration(path, currentConveyorElement.Speed),
                    PathType.Linear
                );
                if (IsPaused)
                {
                    PauseRiding();
                }
            }
        }

        protected virtual float GetDuration(Vector2[] path, float speed)
        {
            float distance = 0;
            for (int i = 0; i < path.Length - PREVIOUS_ELEMENT_INDEX; i++)
            {
                distance += Vector2.Distance(path[i], path[i + PREVIOUS_ELEMENT_INDEX]);
            }
            return GlobalData.SPEED_CONVERT_RATIO / speed / distance;
        }

        protected virtual void SetConveyorElement(ConveyorElement conveyorElement)
        {
            ResetConveyorElement();
            currentConveyorElement = conveyorElement;
            transform.SetParent(currentConveyorElement.transform);
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
        {
            PauseRiding();
            ConveyorController.onLineAddStart -= OnLineAddStarted;
            ConveyorController.onLineAddEnd -= OnLineAddEnded;
        }

        protected virtual void OnDestroy()
            => ResetConveyorElement();
    }
}