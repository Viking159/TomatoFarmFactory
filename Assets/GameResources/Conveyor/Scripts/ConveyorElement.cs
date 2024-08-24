namespace Features.Conveyor
{
    using Features.Extensions.BaseDataTypes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Element of conveyor
    /// </summary>
    public class ConveyorElement : MonoBehaviour
    {
        protected const int PREVIOUS_ELEMENT_INDEX = 1;

        /// <summary>
        /// Speed change event
        /// </summary>
        public event Action onSpeedValueChange = delegate { };

        /// <summary>
        /// Rider count change event
        /// </summary>
        public event Action onRidersCountChange = delegate { };

        /// <summary>
        /// Is valid controller
        /// </summary>
        public virtual bool ValidController => lineController != null;

        /// <summary>
        /// Element index in line elements list
        /// </summary>
        public int Index => ValidController ? lineController.ConveyorLine.conveyorElements.IndexOf(this) : -1;

        /// <summary>
        /// Conveyor element speed
        /// </summary>
        public virtual float Speed => speed;
        protected float speed = default;

        /// <summary>
        /// Riders count
        /// </summary>
        public virtual int RidersCount => ridersCount;
        [SerializeField]
        protected int ridersCount = default;

        /// <summary>
        /// Limit riders count
        /// </summary>
        public virtual int LimitRidersCount => limitRidersCount;
        [SerializeField]
        protected int limitRidersCount = 2;

        /// <summary>
        /// Elemenet's line controller
        /// </summary>
        public ConveyorLineController LineController => lineController;
        protected ConveyorLineController lineController = default;

        [SerializeField]
        protected List<Transform> pathPoints = new List<Transform>();

        protected List<ConveyorRider> riders = new List<ConveyorRider>();
        protected ConveyorController conveyorController = default;
        
        protected ConveyorRider rider = default;

        protected int riderIndex = 0;

        /// <summary>
        /// Init conveyor element
        /// </summary>
        public virtual void Init(ConveyorLineController lineController)
        {
            this.lineController = lineController;
            conveyorController = lineController.LinesController.ConveyorController;
            conveyorController.onLevelChange += SetSpeed;
            SetSpeed();
            SetRidersCount(riders.Count);
        }

        /// <summary>
        /// Get conveyor ride path
        /// </summary>
        public virtual List<Vector2> GetPath(Vector3 riderPosition)
            => pathPoints
            .Select<Transform, Vector2>(pointTransform => pointTransform.position)
            .ToList();

        /// <summary>
        /// Get conveyor ride path from current position
        /// </summary>
        public virtual List<Vector2> GetActualPath(Vector3 riderPosition)
        {
            if (pathPoints.IsNullOrEmpty())
            {
                return new List<Vector2>();
            }
            List<Vector2> path = new List<Vector2>() { riderPosition };
            int firstIndex = pathPoints.Count;
            for (int i = 0; i < pathPoints.Count - PREVIOUS_ELEMENT_INDEX; i++)
            {
                if (riderPosition.ClosestPoint(pathPoints[i].position, pathPoints[i + PREVIOUS_ELEMENT_INDEX].position) 
                    == pathPoints[i].position)
                {
                    firstIndex = i + PREVIOUS_ELEMENT_INDEX;
                    break;
                }
            }
            for (int i = firstIndex; i < pathPoints.Count - PREVIOUS_ELEMENT_INDEX; i++)
            {
                path.Add(pathPoints[i].position);
            }
            path.Add(pathPoints.Last().position);
            return path;
        }

        protected virtual void SetSpeed()
        {
            speed = conveyorController.Speed;
            NotifySpeed();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            rider = collision.GetComponent<ConveyorRider>();
            if (rider != null && !riders.Contains(rider))
            {
                riders.Add(rider);
                SetRidersCount(ridersCount + 1);
                rider.SetConveyorElement(this);
                rider.ResumeRiding(PauseWeight.CONVEYOR_ELEMENT);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            rider = collision.GetComponent<ConveyorRider>();
            riderIndex = riders.IndexOf(rider);
            if (rider != null && riderIndex != -1 && collision.enabled)
            {
                riders.RemoveAt(riderIndex);
                SetRidersCount(ridersCount - 1);
            }
        }

        protected virtual void SetRidersCount(int count)
        {
            count = Mathf.Max(0, count);
            if (ridersCount != count)
            {
                ridersCount = count;
                NotifyRiders();
            }
        }

        protected virtual void NotifySpeed()
            => onSpeedValueChange();

        protected virtual void NotifyRiders()
            => onRidersCountChange();

        protected virtual void OnDisable()
        {
            riders = new List<ConveyorRider>();
            SetRidersCount(0);
        }

        protected virtual void OnDestroy()
        {
            if (conveyorController != null)
            {
                conveyorController.onLevelChange -= SetSpeed;
            }
        }
    }
}