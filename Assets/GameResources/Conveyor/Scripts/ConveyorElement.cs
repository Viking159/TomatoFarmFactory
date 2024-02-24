namespace Features.Conveyor
{
    using Features.Conveyor.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Element of conveyor
    /// </summary>
    public sealed class ConveyorElement : MonoBehaviour
    {
        /// <summary>
        /// Speed change event
        /// </summary>
        public event Action onSpeedValueChange = delegate { };

        /// <summary>
        /// Element start point
        /// </summary>
        public Vector2 StartPoint => _startPoint;
        private Vector2 _startPoint = default;

        /// <summary>
        /// Element end point
        /// </summary>
        public Vector2 EndPoint => _endPoint;
        private Vector2 _endPoint = default;

        /// <summary>
        /// Conveyor element speed
        /// </summary>
        public float Speed => _speed;
        private float _speed = default;

        [SerializeField]
        private ConveyorData _conveyorData = default;
        [SerializeField]
        private Direction _direction = Direction.DownToTop;

        private enum Direction
        {
            LeftToRight,
            RightToLeft,
            DownToTop,
            TopToDown
        }

        private void Awake()
        {
            SetPoints();
            SetSpeed();
        }

        private void SetPoints()
        {
            Vector3 additionalVector = Vector3.zero;
            float additionalXValue = 0;
            float additionalYValue = 0;
            switch (_direction)
            {
                case Direction.LeftToRight:
                    additionalXValue = -transform.lossyScale.y / 2 + transform.lossyScale.x / 2;
                    additionalVector = new Vector3(transform.lossyScale.y / 2, 0);
                    break;
                case Direction.RightToLeft:
                    additionalXValue = transform.lossyScale.y / 2 - transform.lossyScale.x / 2;
                    additionalVector = new Vector3(-transform.lossyScale.y / 2, 0);
                    break;
                case Direction.DownToTop:
                    additionalYValue = -transform.lossyScale.y / 2 + transform.lossyScale.x / 2;
                    additionalVector = new Vector3(0, transform.lossyScale.y / 2);
                    break;
                case Direction.TopToDown:
                    additionalYValue = transform.lossyScale.y / 2 - transform.lossyScale.x / 2;
                    additionalVector = new Vector3(0, -transform.lossyScale.y / 2);
                    break;
            }
            _startPoint = transform.position + new Vector3(additionalXValue, additionalYValue);
            _endPoint = transform.position + additionalVector;
        }

        private void SetSpeed()
        {
            _speed = _conveyorData.DefaultSpeed;
            onSpeedValueChange();
        }
    }
}