namespace Features.Conveyor
{
    using UnityEngine;
    using DG.Tweening;
    using Features.Data;

    /// <summary>
    /// Object on coveyor velocity
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class ConveyorRider : MonoBehaviour
    {
        protected ConveyorElement currentConveyorElement = default;
        protected Tween pathTween = default;
        protected Rigidbody2D rb;

        protected virtual void Awake()
           => rb = GetComponent<Rigidbody2D>();

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ConveyorElement conveyorElement))
            {
                SetConveyorElement(conveyorElement);
            }
        }

        protected virtual void Ride()
        {
            Stop();
            if (currentConveyorElement != null && currentConveyorElement.Speed > 0)
            {
                pathTween = rb.DOPath
                    (
                        currentConveyorElement.GetPath(),
                        GetDuration(currentConveyorElement.Speed),
                        PathType.Linear
                    );
            } 
        }

        protected virtual float GetDuration(float speed)
            => GlobalData.SPEED_CONVERT_RATIO / speed;

        protected virtual void Stop()
        {
            if (pathTween != null)
            {
                pathTween.Kill();
            }
        }

        protected virtual void SetConveyorElement(ConveyorElement conveyorElement)
        {
            ResetConveyorElement();
            currentConveyorElement = conveyorElement;
            currentConveyorElement.onSpeedValueChange += Ride;
            Ride();
        }

        protected virtual void ResetConveyorElement()
        {
            Stop();
            if (currentConveyorElement != null)
            {
                currentConveyorElement.onSpeedValueChange -= Ride;
            }
        }

        protected virtual void OnDestroy()
            => ResetConveyorElement();
    }
}