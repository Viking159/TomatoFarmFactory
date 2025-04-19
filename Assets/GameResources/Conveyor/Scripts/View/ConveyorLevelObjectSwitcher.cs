namespace Features.Conveyor
{
    using Features.Data;
    using Features.Extensions;
    using UnityEngine;

    /// <summary>
    /// Switch object depends on max level
    /// </summary>
    public class ConveyorLevelObjectSwitcher : BaseObjectSwitcher
    {
        [SerializeField]
        protected ConveyorController conveyorController = default;
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetView();
            conveyorController.onDataChange += SetView;
        }

        protected virtual void SetView()
            => SetObjects(conveyorController.Level >= data.MaxLevel);

        protected virtual void OnDisable()
            => conveyorController.onDataChange -= SetView;

    }
}