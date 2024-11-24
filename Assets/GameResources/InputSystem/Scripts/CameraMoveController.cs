namespace Features.InputSystem
{
    using Features.Conveyor;
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem;
    using System.Linq;
    using Features.Extensions.BaseDataTypes;

    /// <summary>
    /// Scroll game view controller
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public sealed class CameraMoveController : MonoBehaviour
    {
        private const int CAMERA_HALF_HEIGHT = 5;

        /// <summary>
        /// Move event (Vector2 is delta)
        /// </summary>
        public event Action<Vector3> onMove = delegate { };

        [SerializeField]
        private Transform _topPosition = default;
        [SerializeField]
        private float _bottomOffset = -1;
        [SerializeField]
        private ConveyorController _conveyorController = default;
        [SerializeField, Min(0)]
        private float _speed = 0.01f;

        private MainInputActions _inputActions = default;
        private float _bottomYPosition = default;
        private Vector3 _newPosition = Vector3.zero;
        private Vector3 _positionDelta = Vector3.zero;

        private void Awake() => _inputActions = new MainInputActions();

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.ActionMap.Enable();
            _inputActions.ActionMap.Move.Enable();
            _inputActions.ActionMap.Move.performed += MovePreformed;
            SetBottomPosition();
            _conveyorController.onLineAddEnd += OnLineAdded;
        }

        private void OnLineAdded() => SetBottomPosition();

        private void SetBottomPosition()
        {
            BaseConveyorLinesController lastLineController = _conveyorController.ConveyorLinesControllers.LastOrDefault();
            if (lastLineController != null)
            {
                ConveyorLineController lastLine = lastLineController.ConveyorLines.IsNullOrEmpty() ? null 
                    : lastLineController.ConveyorLines[lastLineController.IsPositionOrderReversed ? 0 : lastLineController.ConveyorLines.Count - 1];
                _bottomYPosition = (lastLine != null ? lastLine.transform.position.y - lastLine.Height : lastLineController.transform.position.y)
                    + _bottomOffset + CAMERA_HALF_HEIGHT;
                if (_bottomYPosition <= _topPosition.position.y)
                {
                    return;
                }
            }
            _bottomYPosition = _topPosition.position.y;
        }

        private void MovePreformed(InputAction.CallbackContext context)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                MoveCamera(context.ReadValue<float>());
            }
        }

        private void MoveCamera(float delta)
        {
            _newPosition = new Vector3(transform.position.x, transform.position.y + delta * _speed, transform.position.z);
            ClampNewPosition();
            if (_newPosition != transform.position)
            {
                _positionDelta = _newPosition - transform.position;
                transform.position = _newPosition;
                onMove(_positionDelta);
            }
        }

        private void ClampNewPosition()
        {
            if (_newPosition.y < _bottomYPosition)
            {
                _newPosition.y = _bottomYPosition;
                return;
            }
            if (_newPosition.y > _topPosition.position.y)
            {
                _newPosition.y = _topPosition.position.y;
            }
        }

        private void OnDisable()
        {
            _inputActions.ActionMap.Move.performed -= MovePreformed;
            _inputActions.ActionMap.Move.Disable();
            _inputActions.ActionMap.Disable();
            _inputActions.Disable();
            _conveyorController.onLineAddEnd -= OnLineAdded;
        }
    }
}