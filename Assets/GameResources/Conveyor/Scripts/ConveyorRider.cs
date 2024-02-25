namespace Features.Conveyor
{
    using UnityEngine;
    using DG.Tweening;

    /// <summary>
    /// Object on coveyor velocity
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public sealed class ConveyorRider : MonoBehaviour
    {
        private ConveyorElement _currentConveyorElement = default;
        private Tween _pathTween = default;
        private Rigidbody2D _rb;

        private const float SPEED_RATIO_NOMINATOR = 10f;

        private void Awake()
           => _rb = GetComponent<Rigidbody2D>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ConveyorElement conveyorElement))
            {
                SetConveyorElement(conveyorElement);
            }
        }

        private void Ride()
        {
            Stop();
            if (_currentConveyorElement != null && _currentConveyorElement.Speed > 0)
                _pathTween = _rb.DOPath
                    (
                        new Vector2[] { _currentConveyorElement.StartPoint, _currentConveyorElement.EndPoint },
                        GetDuration(_currentConveyorElement.Speed),
                        PathType.Linear
                    );
        }

        private float GetDuration(float speed)
            => SPEED_RATIO_NOMINATOR / speed;


        private void Stop()
        {
            if (_pathTween != null)
            {
                _pathTween.Kill();
            }
        }

        private void SetConveyorElement(ConveyorElement conveyorElement)
        {
            ResetConveyorElement();
            _currentConveyorElement = conveyorElement;
            _currentConveyorElement.onSpeedValueChange += Ride;
            Ride();
        }

        private void ResetConveyorElement()
        {
            if (_currentConveyorElement != null)
            {
                Stop();
                _currentConveyorElement.onSpeedValueChange -= Ride;
            }
        }

        private void OnDestroy()
        {
            ResetConveyorElement();
        }
    }
}