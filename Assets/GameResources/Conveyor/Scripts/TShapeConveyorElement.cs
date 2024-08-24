namespace Features.Conveyor
{
    using System.Collections.Generic;
    using UnityEngine;
    using Features.Extensions.BaseDataTypes;
    using System.Linq;

    /// <summary>
    /// T-shape element of conveyor
    /// </summary>
    public class TShapeConveyorElement : ConveyorElement
    {
        [SerializeField]
        protected Transform startPoint1 = default;
        [SerializeField]
        protected Transform startPoint2 = default;

        protected Vector3 startPosition = default;

        public override List<Vector2> GetPath(Vector3 riderPosition)
        {
            SetStartPoint(riderPosition);
            List<Vector2> points = new List<Vector2>(pathPoints.Count + 1)
            {
                startPosition
            };
            points.AddRange(base.GetPath(riderPosition));
            return points;
        }

        public override List<Vector2> GetActualPath(Vector3 riderPosition)
        {
            SetStartPoint(riderPosition);
            if (pathPoints.IsNullOrEmpty())
            {
                return new List<Vector2>();
            }
            List<Vector2> path = new List<Vector2>() { riderPosition };
            int firstIndex = pathPoints.Count;
            if (riderPosition.ClosestPoint(startPosition, pathPoints[0].position) == startPosition)
            {
                firstIndex = 0;
            }
            else
            {
                for (int i = 0; i < pathPoints.Count - PREVIOUS_ELEMENT_INDEX; i++)
                {
                    if (riderPosition.ClosestPoint(pathPoints[i].position, pathPoints[i + PREVIOUS_ELEMENT_INDEX].position)
                        == pathPoints[i].position)
                    {
                        firstIndex = i + PREVIOUS_ELEMENT_INDEX;
                        break;
                    }
                }
            }
            for (int i = firstIndex; i < pathPoints.Count - PREVIOUS_ELEMENT_INDEX; i++)
            {
                path.Add(pathPoints[i].position);
            }
            path.Add(pathPoints.Last().position);
            return path;
        }

        protected virtual void SetStartPoint(Vector3 riderPosition) 
            => startPosition = riderPosition.ClosestPoint(startPoint1.position, startPoint2.position);
    }
}
