namespace Features.Conveyor
{
    using UnityEngine;
    using System.Collections.Generic;
    using Features.Extensions.BaseDataTypes;
    using System.Linq;

    /// <summary>
    /// Base conveyor line controller
    /// </summary>
    public class BaseConveyorLinesController : MonoBehaviour
    {
        protected const int MIN_ELEMENTS_COUNT = 1;
        protected const int HALF_DENOMINATOR = 2;

        /// <summary>
        /// Is valid controller
        /// </summary>
        public virtual bool ValidController => conveyorController != null;

        /// <summary>
        /// Conveyor part index in conveyor parts list
        /// </summary>
        public virtual int Index => ValidController ? conveyorController.ConveyorLinesControllers.IndexOf(this) : -1;

        /// <summary>
        /// Prefab height
        /// </summary>
        public virtual float PrefabHeight => conveyorLinePrefab.Height;

        /// <summary>
        /// Conveyor lines
        /// </summary>
        public IReadOnlyList<ConveyorLineController> ConveyorLines => conveyorLines;
        [SerializeField]
        protected List<ConveyorLineController> conveyorLines = new List<ConveyorLineController>();

        /// <summary>
        /// Is lines positions reversed
        /// </summary>
        public bool IsPositionOrderReversed => isPositionOrderReversed;
        [SerializeField]
        protected bool isPositionOrderReversed = false;

        /// <summary>
        /// Conveyor controller
        /// </summary>
        public ConveyorController ConveyorController => conveyorController;
        protected ConveyorController conveyorController = default;

        [SerializeField]
        protected Transform lineTransform = default;
        [SerializeField]
        protected ConveyorLineController conveyorLinePrefab = default;
        [SerializeField]
        protected Vector3 startPosition = Vector3.zero;

        /// <summary>
        /// Init conveyor lines
        /// </summary>
        public virtual void InitLines(ConveyorController conveyorController)
        {
            this.conveyorController = conveyorController;
            foreach (ConveyorLineController conveyorLine in conveyorLines)
            {
                conveyorLine.InitConveyorLine(this);
            }
        }

        /// <summary>
        /// Add conveyor line
        /// </summary>
        public virtual void AddLine()
        {
            conveyorLines.Add(Instantiate(conveyorLinePrefab, lineTransform));
            SetLastLinePosition();
        }

        protected virtual void SetLastLinePosition()
        {
            if (conveyorLines.Count > MIN_ELEMENTS_COUNT)
            {
                SetNewLinePosition(conveyorLines.Last(), conveyorLines.BeforeLast());
            }
            else
            {
                SetFirstPosition(conveyorLines.Last());
            }
        }

        /// <summary>
        /// Move lines
        /// </summary>
        public virtual void MoveLines() => MoveLines(conveyorLinePrefab.Height);

        /// <summary>
        /// Move lines
        /// </summary>
        public virtual void MoveLines(float height) 
            => lineTransform.transform.position = new Vector3
            (
                lineTransform.transform.position.x, 
                lineTransform.transform.position.y - height, 
                lineTransform.transform.position.z
            );

        protected virtual void SetNewLinePosition(ConveyorLineController newElement, ConveyorLineController lastLineController)
            => newElement.transform.position = new Vector3
            (
                lastLineController.transform.position.x,
                lastLineController.transform.position.y - newElement.Height,
                lastLineController.transform.position.z
            );

        protected virtual void SetFirstPosition(ConveyorLineController newElement) => newElement.transform.localPosition = startPosition;
    }
}