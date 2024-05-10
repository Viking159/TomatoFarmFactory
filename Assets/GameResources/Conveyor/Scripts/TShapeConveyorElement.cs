namespace Features.Conveyor
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// T-shape element of conveyor
    /// </summary>
    public class TShapeConveyorElement : ConveyorElement
    {
        [SerializeField]
        protected Transform startPoint1 = default;
        [SerializeField]
        protected Transform startPoint2 = default;

        public override Vector2[] GetPath(Vector3 riderPosition)
        {
            List<Vector2> points = new List<Vector2>(pathPoints.Count + 1)
            {
                Vector2.Distance(riderPosition, startPoint1.position) < Vector2.Distance(riderPosition, startPoint2.position)
                ? startPoint1.position
                : startPoint2.position
            };
            points.AddRange(base.GetPath(riderPosition));
            return points.ToArray();
        }
    }
}
