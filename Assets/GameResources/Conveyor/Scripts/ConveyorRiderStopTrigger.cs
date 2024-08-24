namespace Features.Conveyor
{
    using Features.Extensions.BaseDataTypes;
    using System;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Conveyor collision on conveyor to stop
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ConveyorRiderStopTrigger : MonoBehaviour
    {
        protected const float EPSILON = 0.1f;

        protected ConveyorRider conveyorRider = default;
        protected ConveyorRider collisionConveyorRider = default;

        protected int witingRiderId = default;

        protected virtual void Awake() => conveyorRider = GetComponentInParent<ConveyorRider>();

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out collisionConveyorRider)
                && ValidStopCondition(collisionConveyorRider))
            {
                witingRiderId = collisionConveyorRider.GetInstanceID();
                conveyorRider.PauseRiding(PauseWeight.RIDERS_COLLISION);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out collisionConveyorRider)
                && witingRiderId == collisionConveyorRider.GetInstanceID())
            {
                witingRiderId = default;
                conveyorRider.ResumeRiding(PauseWeight.RIDERS_COLLISION);
            }
        }

        /// <summary>
        /// Valid stop because of collisionConveyorRider
        /// </summary>
        protected virtual bool ValidStopCondition(ConveyorRider collisionConveyorRider)
        {
            if (conveyorRider.CurrentConveyorElement != collisionConveyorRider.CurrentConveyorElement 
                && ValidConveyorControllers(conveyorRider.CurrentConveyorElement) && ValidConveyorControllers(collisionConveyorRider.CurrentConveyorElement))
            {
                return ValidConveyorElementsPosition(conveyorRider.CurrentConveyorElement, collisionConveyorRider.CurrentConveyorElement);
            }
            if (conveyorRider.IsPathInited)
            {
                return ValidConveyorCondition(conveyorRider.PathStartPoint, conveyorRider.PathFinalPoint, collisionConveyorRider);
            }
            Debug.LogError($"{nameof(ConveyorRiderStopTrigger)}: Valid stop condition error");
            return false;
        }

        protected virtual bool ValidConveyorControllers(ConveyorElement conveyorElement)
            => conveyorElement != null && conveyorElement.ValidController
            && conveyorElement.LineController.ValidController && conveyorElement.LineController.LinesController.ValidController;

        protected virtual bool ValidConveyorElementsPosition(ConveyorElement riderElement, ConveyorElement collisionElement)
        {
            if (riderElement.LineController.LinesController == collisionElement.LineController.LinesController)
            {
                if (riderElement.LineController == collisionElement.LineController)
                {
                    return riderElement.Index > collisionElement.Index;
                }
                return riderElement.LineController.Index > collisionElement.LineController.Index;
            }
            return riderElement.LineController.LinesController.Index > collisionElement.LineController.LinesController.Index;
        }

        protected virtual bool ValidConveyorCondition(Vector3 startPoint, Vector3 endPoint, ConveyorRider collisionConveyorRider)
        {
            if (startPoint.y.ApproximatelyCompare(endPoint.y) != 0)
            {
                return VilidLineConveyorPosition(startPoint, endPoint, collisionConveyorRider, (vector) => vector.y);
            }
            return VilidLineConveyorPosition(startPoint, endPoint, collisionConveyorRider, (vector) => vector.x);
        }

        protected virtual bool VilidLineConveyorPosition(Vector3 startPoint, Vector3 endPoint, ConveyorRider collisionConveyorRider, Func<Vector2, float> func)
            => func(startPoint).ApproximatelyCompare(func(endPoint)) 
            == func(conveyorRider.transform.position).ApproximatelyCompare(func(collisionConveyorRider.transform.position));
    }
}