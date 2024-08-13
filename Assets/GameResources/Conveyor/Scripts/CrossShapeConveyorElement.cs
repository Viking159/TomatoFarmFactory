namespace Features.Conveyor
{
    using UnityEngine;
    using Features.Extensions.BaseDataTypes;

    /// <summary>
    /// Cross shape element of conveyor
    /// </summary>
    public class CrossShapeConveyorElement : TShapeConveyorElement
    {
        [SerializeField]
        protected Transform startPoint3 = default;

        protected override void SetStartPoint(Vector3 riderPosition)
        {
            base.SetStartPoint(riderPosition);
            startPosition = riderPosition.ClosestPoint(startPosition, startPoint3.position);
        }
    }
}