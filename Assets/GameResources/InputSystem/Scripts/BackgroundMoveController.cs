using UnityEngine;

namespace Features.InputSystem
{
    public sealed class BackgroundMoveController : MonoBehaviour
    {
        [SerializeField]
        private CameraMoveController _moveController = default;

        private void OnEnable() => _moveController.onMove += MoveHandler;

        private void MoveHandler(Vector3 vector) => transform.position -= vector;

        private void OnDisable() => _moveController.onMove -= MoveHandler;
    }
}
