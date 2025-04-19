namespace Features.InfoBox
{
    using Features.Conveyor;
    using System;

    public class ConveyorInfoBox : InfoBox
    {
        /// <summary>
        /// Change data event
        /// </summary>
        public event Action onDataChange = delegate { };

        public virtual ConveyorController ConveyorController { get; protected set; } = default;

        public virtual void InitData(ConveyorController conveyorController)
        {
            ConveyorController = conveyorController;
            NotifyOnDataChange();
            ConveyorController.onDataChange += NotifyOnDataChange;
        }

        protected virtual void NotifyOnDataChange() => onDataChange();

        protected virtual void OnDisable()
        {
            if (ConveyorController != null)
            {
                ConveyorController.onDataChange -= NotifyOnDataChange;
            }
        }
    }
}