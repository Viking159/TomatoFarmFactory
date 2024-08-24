namespace Features.Conveyor
{
    using UnityEngine;
    using Features.Extensions.BaseDataTypes;
    using System.Collections.Generic;

    /// <summary>
    /// Cross shape element of conveyor
    /// </summary>
    public class CrossShapeConveyorElement : TShapeConveyorElement
    {
        protected const int MIN_QUEUE = 2;

        [SerializeField]
        protected Transform startPoint3 = default;
        protected List<ConveyorRider> riders = new List<ConveyorRider>();

        protected int riderIndex = 0;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            rider = collision.GetComponent<ConveyorRider>();
            if (rider != null && !riders.Contains(rider))
            {
                SetRidersCount(ridersCount + 1);
                rider.SetConveyorElement(this);
                riders.Add(rider);
                if (riders.Count < MIN_QUEUE)
                {
                    rider.ResumeRiding(PauseWeight.CROSSROAD);
                }
                else
                {
                    rider.PauseRiding(PauseWeight.CROSSROAD);
                }
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);
            riderIndex = riders.IndexOf(rider);
            if (rider != null && riderIndex != -1 && (collision.enabled || collision.gameObject == null || !collision.gameObject.activeInHierarchy))
            {
                riders.RemoveAt(riderIndex);
                if (!riders.IsNullOrEmpty())
                {
                    riders[0].ResumeRiding(PauseWeight.CROSSROAD);
                }
            }
        }

        protected override void SetStartPoint(Vector3 riderPosition)
        {
            base.SetStartPoint(riderPosition);
            startPosition = riderPosition.ClosestPoint(startPosition, startPoint3.position);
        }
    }
}