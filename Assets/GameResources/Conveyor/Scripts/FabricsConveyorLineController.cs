namespace Features.Conveyor
{
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Fabric conveyor line controller
    /// </summary>
    public class FabricsConveyorLineController : BaseConveyorLinesController
    {
        protected override void SetLastLinePosition()
        {
            MoveInnerLines();
            SetFirstPosition(conveyorLines.Last());
        }

        protected virtual void MoveInnerLines()
        {
            for (int i = 0; i < conveyorLines.Count; i++)
            {
                conveyorLines[i].transform.position = new Vector3
                    (
                        conveyorLines[i].transform.position.x,
                        conveyorLines[i].transform.position.y - conveyorLinePrefab.Height,
                        conveyorLines[i].transform.position.z
                    );
            }
        }
    }
}

