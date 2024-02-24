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
        [SerializeField]
        private float _ySpeed = 0;

        private ConveyorElement _currentConveyorElement = default;
        private Tween _pathTween = default;
        private Rigidbody2D _rb;

        private void Awake()
           => _rb = GetComponent<Rigidbody2D>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Triggered");
            if (collision.TryGetComponent(out ConveyorElement conveyorElement))
            {
                SetConveyorElement(conveyorElement);
            }
        }

        private void Update()
        {
            _rb.velocity = new Vector2(0, _ySpeed);
        }

        private void Ride()
        {
            Stop();
            _pathTween = _rb.DOPath(
                new Vector2[] { _currentConveyorElement.StartPoint, _currentConveyorElement.EndPoint },
                GetDuration(_currentConveyorElement.Speed),
                PathType.Linear);
        }

        private float GetDuration(float speed)
            => 1 / speed;


        private void Stop()
        {
            if (_pathTween != null)
            {
                _pathTween.Kill();
            }
        }

        private void SetConveyorElement(ConveyorElement conveyorElement)
        {
            Debug.Log("setting");
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