namespace Features.Conveyor
{
    using UnityEngine;
    using Features.Conveyor.Data;
    using Features.Extensions.BaseDataTypes;
    using System.Collections.Generic;
    using System;
    using System.Collections;

    /// <summary>
    /// Conveyor controller
    /// </summary>
    public class ConveyorController : MonoBehaviour
    {
        protected const int MIN_LIST_INDEX = 0;
        protected const int MIN_ELEMENTS_COUNT = 1;

        /// <summary>
        /// Start adding line event
        /// </summary>
        public static event Action onLineAddStart = delegate { };

        /// <summary>
        /// End adding line event
        /// </summary>
        public static event Action onLineAddEnd = delegate { };

        /// <summary>
        /// Conveyor level change event
        /// </summary>
        public event Action onLevelChange = delegate { };

        /// <summary>
        /// Conveyor level
        /// </summary>
        public virtual int Level => conveyorData.Level;

        /// <summary>
        /// Conveyor speed
        /// </summary>
        public virtual float Speed => conveyorData.Speed;

        [SerializeField, Min(0)]
        protected float addLineTimeAwait = 0.5f;
        [SerializeField]
        protected ConveyorData conveyorData = default;
        [SerializeField]
        protected List<BaseConveyorLinesController> conveyorLinesControllers = new List<BaseConveyorLinesController>();

        protected ConveyorLineController currentLine = default;
        protected Coroutine addLineCoroutine = default;

        /// <summary>
        /// Add conveyor lines
        /// </summary>
        /// <param name="conveyorLineControllerIndex"></param>
        public virtual void AddLines(int conveyorLineControllerIndex)
        {
            if (conveyorLinesControllers.IsValidIndex(conveyorLineControllerIndex))
            {
                addLineCoroutine = StartCoroutine(AddLineWithAwait(conveyorLineControllerIndex));
            }
        }


        protected virtual void Awake()
        {
            conveyorData.onDataChange += Notfity;
            InitLinesControllers();
        }

        protected virtual void InitLinesControllers()
        {
            foreach (BaseConveyorLinesController conveyorLinesController in conveyorLinesControllers)
            {
                conveyorLinesController.InitLines(this);
            }
        }

        protected virtual IEnumerator AddLineWithAwait(int conveyorLineControllerIndex)
        {
            NotifyOnLineAddStart();
            MoveLines(conveyorLineControllerIndex, conveyorLineControllerIndex + 1);
            conveyorLinesControllers[conveyorLineControllerIndex].AddLine();
            yield return new WaitForSeconds(addLineTimeAwait);
            InitLinesControllers();
            NotifyOnLineAddEnd();
        }

        protected virtual void MoveLines(int insertIndex, int startIndex)
        {
            if (conveyorLinesControllers.IsValidIndex(insertIndex))
            {
                for (int i = startIndex; i < conveyorLinesControllers.Count; i++)
                {
                    conveyorLinesControllers[i].MoveLines(conveyorLinesControllers[insertIndex].PrefabHeight);
                }
            }
        }

        protected virtual void Notfity() => onLevelChange();

        protected virtual void NotifyOnLineAddStart() => onLineAddStart();

        protected virtual void NotifyOnLineAddEnd() => onLineAddEnd();

        protected virtual void OnDisable()
        {
            if (addLineCoroutine != null)
            {
                StopCoroutine(addLineCoroutine);
            }
        }

        protected virtual void OnDestroy()
            => conveyorData.onDataChange -= Notfity;
    }
}