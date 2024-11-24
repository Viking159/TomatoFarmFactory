namespace Features.SpawnerAddStaticActionController
{
    using Features.Conveyor;
    using Features.SaveSystem;
    using UnityEngine;

    /// <summary>
    /// Spawner add handler calls static data update  action
    /// </summary>
    [RequireComponent(typeof(ConveyorLineController))]
    public class SpawnerAddStaticActionController : MonoBehaviour
    {
        protected ConveyorLineController conveyorLineController = default;

        protected virtual void Awake() => conveyorLineController = GetComponent<ConveyorLineController>();

        protected virtual void OnEnable() => conveyorLineController.onSpawnerAdd += InvokeEvent;

        protected virtual void InvokeEvent() => UpdateDataStaticAction.NotifyUpdateRequire();

        protected virtual void OnDisable() => conveyorLineController.onSpawnerAdd -= InvokeEvent;
    }
}