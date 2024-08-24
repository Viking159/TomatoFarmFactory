namespace Features.Conveyor
{
    using UnityEngine;
    using DG.Tweening;
    using Features.Data;
    using System;
    using Features.Spawner;
    using System.Collections.Generic;
    using Features.Extensions.BaseDataTypes;
    using System.Linq;

    /// <summary>
    /// Pause weights
    /// </summary>
    public enum PauseWeight
    {
        NOT_PAUSED_VALUE,
        DEFAULT,
        RIDERS_COLLISION = 10,
        CROSSROAD = 30,
        CONVEYOR_ELEMENT = 50,
        FORCE = 999
    }

    /// <summary>
    /// Object on coveyor velocity
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class ConveyorRider : MonoBehaviour
    {
        protected const int PREVIOUS_ELEMENT_INDEX = 1;

        /// <summary>
        /// Ride state change event
        /// </summary>
        public event Action onRideStateChange = delegate { };

        /// <summary>
        /// Is ride paused
        /// </summary>
        public virtual bool IsPaused => pauseWeight != PauseWeight.NOT_PAUSED_VALUE;

        /// <summary>
        /// Is riders path inited
        /// </summary>
        public virtual bool IsPathInited => !path.IsNullOrEmpty();

        /// <summary>
        /// Riders start point
        /// </summary>
        public virtual Vector2 PathStartPoint => path.IsNullOrEmpty() ? Vector2.negativeInfinity : path[0];

        /// <summary>
        /// Riders final point
        /// </summary>
        public virtual Vector2 PathFinalPoint => path.IsNullOrEmpty() ? Vector2.negativeInfinity : path.Last();

        protected ConveyorRiderTriggerController conveyorRiderCollisionController = default;
        protected ConveyorElement currentConveyorElement = default;
        protected Tween pathTween = default;
        protected Rigidbody2D rb = default;
        protected Collider2D riderCollider = default;
        protected List<Vector2> path = new List<Vector2>();
        protected List<Vector2> newPath = new List<Vector2>();

        protected PauseWeight pauseWeight = PauseWeight.NOT_PAUSED_VALUE;
        protected PauseWeight prevPauseWeight = PauseWeight.NOT_PAUSED_VALUE;

        /// <summary>
        /// Resume riding
        /// </summary>
        public virtual void ResumeRiding(PauseWeight unpauseWeight)
        {
            if (!isActiveAndEnabled || pauseWeight > unpauseWeight)
            {
                return;
            }
            ResumeRide();
            pauseWeight = PauseWeight.NOT_PAUSED_VALUE;
            NotifyOnRideStateChange();
        }

        /// <summary>
        /// Pause riding
        /// </summary>
        public virtual void PauseRiding(PauseWeight pauseWeight)
        {
            if (pauseWeight == PauseWeight.NOT_PAUSED_VALUE || this.pauseWeight >= pauseWeight)
            {
                return;
            }
            prevPauseWeight = this.pauseWeight;
            this.pauseWeight = pauseWeight;
            KillRider();
            if (prevPauseWeight == PauseWeight.NOT_PAUSED_VALUE)
            {
                NotifyOnRideStateChange();
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
                pathTween = null;
            }
        }

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            riderCollider = GetComponent<Collider2D>();
            conveyorRiderCollisionController = GetComponentInChildren<ConveyorRiderTriggerController>();
        }

        protected virtual void OnEnable()
        {
            ResumeRiding(PauseWeight.DEFAULT);
            ConveyorController.AddLineAddingStartListener(OnLineAddStarted);
            ConveyorController.AddLineAddingEndListener(OnLineAddEnded);
        }

        protected virtual void OnLineAddStarted()
        {
            prevPauseWeight = pauseWeight;
            SetComponents(false);
            PauseRiding(PauseWeight.FORCE);
            transform.SetParent(currentConveyorElement.transform);
        }

        protected virtual void OnLineAddEnded()
        {
            transform.SetParent(null);
            pauseWeight = prevPauseWeight;
            path = currentConveyorElement == null ? new List<Vector2>()
                : currentConveyorElement.GetPath(transform.position);
            ResumeRiding(PauseWeight.DEFAULT);
            SetComponents(true);
        }

        protected virtual void SetComponents(bool status)
        {
            riderCollider.enabled = status;
            conveyorRiderCollisionController.enabled = status;
        }

        protected virtual void Ride()
        {
            KillRider();
            if (currentConveyorElement != null && currentConveyorElement.Speed > 0)
            {
                pathTween = rb.DOPath
                (
                    path.ToArray(),
                    GetDuration(currentConveyorElement.Speed),
                    PathType.Linear
                );
            }
        }

        protected virtual void ResumeRide()
        {
            KillRider();
            if (currentConveyorElement != null && currentConveyorElement.Speed > 0)
            {
                pathTween = rb.DOPath
                (
                    currentConveyorElement.GetActualPath(transform.position).ToArray(),
                    GetDuration(currentConveyorElement.Speed),
                    PathType.Linear
                );
            }
        }

        protected virtual float GetDuration(float speed)
        {
            float distance = 0;
            for (int i = 0; i < path.Count - PREVIOUS_ELEMENT_INDEX; i++)
            {
                distance += Vector2.Distance(path[i], path[i + PREVIOUS_ELEMENT_INDEX]);
            }
            return GlobalData.SPEED_CONVERT_RATIO / speed / distance;
        }

        

        public virtual void SetConveyorElement(ConveyorElement conveyorElement)
        {
            if (currentConveyorElement == conveyorElement)
            {
                return;
            }
            newPath = conveyorElement.GetPath(transform.position);
            if (!IsPathInited || newPath[0].ApproximatelyEqualsVectors(path.Last()))
            {
                ResetConveyorElement();
                currentConveyorElement = conveyorElement;
                path = newPath;
                currentConveyorElement.onSpeedValueChange += OnConveyorSpeedChanged;
            }
        }

        protected virtual void OnConveyorSpeedChanged()
        {
            if (!IsPaused)
            {
                ResumeRiding(PauseWeight.DEFAULT);
            }
        }

        protected virtual void ResetConveyorElement()
        {
            KillRider();
            if (currentConveyorElement != null)
            {
                currentConveyorElement.onSpeedValueChange -= OnConveyorSpeedChanged;
            }
        }

        protected virtual void NotifyOnRideStateChange() => onRideStateChange();

        protected virtual void OnDisable()
        {
            PauseRiding(PauseWeight.DEFAULT);
            ConveyorController.RemoveLineAddingStartListener(OnLineAddStarted);
            ConveyorController.RemoveLineAddingEndListener(OnLineAddEnded);
        }

        protected virtual void OnDestroy()
            => ResetConveyorElement();
    }
}