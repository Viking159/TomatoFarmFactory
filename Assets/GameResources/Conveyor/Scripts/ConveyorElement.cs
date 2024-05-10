namespace Features.Conveyor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Element of conveyor
    /// </summary>
    public class ConveyorElement : MonoBehaviour
    {
        /// <summary>
        /// Speed change event
        /// </summary>
        public event Action onSpeedValueChange = delegate { };

        /// <summary>
        /// Rider count change event
        /// </summary>
        public event Action onRidersCountChange = delegate { };

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
        [SerializeField]
        protected List<Transform> pathPoints = new List<Transform>();
        
        protected ConveyorController conveyorController = default;

        /// <summary>
        /// Init conveyor element
        /// </summary>
        public virtual void Init(ConveyorController conveyorController)
        {
            this.conveyorController = conveyorController;
            this.conveyorController.onLevelChange += SetSpeed;
            SetSpeed();
            SetRidersCount(0);
        }

        /// <summary>
        /// Get conveoyr ride path
        /// </summary>
        public virtual Vector2[] GetPath(Vector3 riderPosition)
            => pathPoints
            .Select<Transform, Vector2>(pointTransform => pointTransform.position)
            .ToArray();

        protected virtual void SetSpeed()
        {
            speed = conveyorController.Speed;
            NotifySpeed();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<ConveyorRider>() != null)
            {
                SetRidersCount(ridersCount + 1);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<ConveyorRider>() != null)
            {
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

        protected virtual void OnDestroy()
        {
            if (conveyorController != null)
            {
                conveyorController.onLevelChange -= SetSpeed;
            }
        }
    }
}