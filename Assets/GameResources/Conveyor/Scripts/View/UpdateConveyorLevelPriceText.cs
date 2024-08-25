namespace Features.Conveyor
{
    using Features.Data;
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Update conveyor level price text
    /// </summary>
    public class UpdateConveyorLevelPriceText : MaskedTextView
    {
        [SerializeField]
        protected ConveyorController conveyorController = default;
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetText();
            conveyorController.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(data.GetUpdateLevelPrice(conveyorController.Level));

        protected virtual void OnDisable()
            => conveyorController.onDataChange -= SetText;
    }
}