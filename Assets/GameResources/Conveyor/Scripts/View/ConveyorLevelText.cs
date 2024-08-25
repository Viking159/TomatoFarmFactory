namespace Features.Conveyor
{
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Conveyor level text
    /// </summary>
    public class ConveyorLevelText : MaskedTextView
    {
        [SerializeField]
        protected ConveyorController conveyorController = default;

        protected virtual void OnEnable()
        {
            SetText();
            conveyorController.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(conveyorController.Level);

        protected virtual void OnDisable()
            => conveyorController.onDataChange -= SetText;
    }
}