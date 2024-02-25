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
        public Vector2 StartPoint => _startPointTranform.position;

        /// <summary>
        /// Element end point
        /// </summary>
        public Vector2 EndPoint => _endPointTranform.position;

        /// <summary>
        /// Conveyor element speed
        /// </summary>
        public float Speed => _speed;
        private float _speed = default;

        [SerializeField]
        private Transform _startPointTranform = default;
        [SerializeField]
        private Transform _endPointTranform = default;

        private ConveyorController _conveyorController = default;

        /// <summary>
        /// Init conveyor element
        /// </summary>
        /// <param name="conveyorController">controller</param>
        public void Init(ConveyorController conveyorController)
        {
            _conveyorController = conveyorController;
            _conveyorController.onLevelChange += SetSpeed;
            SetSpeed();
        }

        private void SetSpeed()
        {
            _speed = _conveyorController.Speed;
            onSpeedValueChange();
        }

        private void OnDestroy()
        {
            if (_conveyorController != null)
                _conveyorController.onLevelChange -= SetSpeed;
        }
    }
}