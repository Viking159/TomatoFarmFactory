namespace Features.ConstructPlace
{
    using Features.Conveyor;
    using UnityEngine;

    /// <summary>
    /// Construct place on conveyor line controller
    /// </summary>
    public class ConstructPlaceOnLineController : ConstructPlaceController
    {
        [SerializeField]
        protected ConveyorLineController conveyorLineController = default;

        protected override void CreateConstruction()
        {
            base.CreateConstruction();
            conveyorLineController.AddSpawner(construction);
        }
    }
}