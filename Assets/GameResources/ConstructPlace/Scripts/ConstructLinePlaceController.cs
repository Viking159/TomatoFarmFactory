namespace Features.ConstructPlace
{
    using Features.Conveyor;
    using UnityEngine;

    /// <summary>
    /// Construct place controller with no self destroy
    /// </summary>
    public class ConstructLinePlaceController : ConstructPlaceController
    {
        [SerializeField]
        protected ConveyorController conveyorController = default;
        [SerializeField, Min(0)]
        protected int lineIndex = 1;

        /// <summary>
        /// Create consturction 
        /// </summary>
        public override void ConstructPlace()
        {
            if (IsValidTime)
            {
                lastConstructTime = Time.time;
                CreateConstruction();
            }
        }

        protected override void CreateConstruction() => conveyorController.AddLines(lineIndex);
    }
}