namespace Features.Conveyor
{
    using UnityEngine;
    using Features.Conveyor.Data;
    using Features.Extensions.BaseDataTypes;
    using System.Collections.Generic;
    using System;
    using System.Collections;
    using Features.SaveSystem;

    /// <summary>
    /// Conveyor controller
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class ConveyorController : MonoBehaviour
    {
        protected const int MIN_LIST_INDEX = 0;
        protected const int MIN_ELEMENTS_COUNT = 1;

        /// <summary>
        /// Data change event
        /// </summary>
        public event Action onDataChange = delegate { };

        /// <summary>
        /// Start adding line event
        /// </summary>
        public event Action onLineAddStart = delegate { };

        /// <summary>
        /// End adding line event
        /// </summary>
        public event Action onLineAddEnd = delegate { };

        /// <summary>
        /// Conveyor level
        /// </summary>
        public virtual int Level => data.Level;
        
        /// <summary>
        /// Conveyor speed
        /// </summary>
        public virtual float Speed => conveyorData.GetSpeed(data.Level);

        /// <summary>
        /// Adding line index
        /// </summary>
        public int AddingLineIndex { get; protected set; } = 0;

        public IReadOnlyList<BaseConveyorLinesController> ConveyorLinesControllers => conveyorLinesControllers;
        [SerializeField]
        protected List<BaseConveyorLinesController> conveyorLinesControllers = new List<BaseConveyorLinesController>();

        /// <summary>
        /// Data
        /// </summary>
        public ConveyorControllerData Data => data;
        protected ConveyorControllerData data = new ConveyorControllerData();

        [SerializeField]
        protected bool awaitTimeOnLineAdd = true;
        [SerializeField, Min(0)]
        protected float addLineTimeAwait = 0.5f;
        [SerializeField]
        protected ConveyorData conveyorData = default;

        
        protected ConveyorLineController currentLine = default;
        protected Coroutine addLineCoroutine = default;
        
        #region Instance line add events controlls
        protected static ConveyorController instance = default;

        /// <summary>
        /// Listen line add start event
        /// </summary>
        public static void AddLineAddingStartListener(Action callback)
        {
            if (instance != null)
            {
                instance.onLineAddStart += callback;
            }
        }

        /// <summary>
        /// Listen line add end event
        /// </summary>
        public static void AddLineAddingEndListener(Action callback)
        {
            if (instance != null)
            {
                instance.onLineAddEnd += callback;
            }
        }

        /// <summary>
        /// Stop listen line add start event
        /// </summary>
        public static void RemoveLineAddingStartListener(Action callback)
        {
            if (instance != null)
            {
                instance.onLineAddStart -= callback;
            }
        }

        /// <summary>
        /// Stop listen line add end event
        /// </summary>
        public static void RemoveLineAddingEndListener(Action callback)
        {
            if (instance != null)
            {
                instance.onLineAddEnd -= callback;
            }
        }
        #endregion

        /// <summary>
        /// Init level
        /// </summary>
        public virtual void InitData(ConveyorControllerData data)
        {
            this.data = data;
            NotfityOnDataChange();
        }

        public virtual void SetLevel(int level)
        {
            int newLevel = Mathf.Clamp(level, 0, conveyorData.MaxLevel);
            if (data.Level != newLevel)
            {
                data.Level = level;
                NotfityOnDataChange();
            }
        }

        /// <summary>
        /// Init lines controllers
        /// </summary>
        public virtual void InitLinesControllers()
        {
            foreach (BaseConveyorLinesController conveyorLinesController in conveyorLinesControllers)
            {
                conveyorLinesController.InitLines(this);
            }
            NotifyOnLineAddEnd();
        }

        /// <summary>
        /// Add conveyor lines immediately and without events
        /// </summary>
        public virtual void AddLinesImmediate(int conveyorLineControllerIndex)
        {
            AddingLineIndex = conveyorLineControllerIndex;
            MoveLines(conveyorLineControllerIndex, conveyorLineControllerIndex + 1);
            conveyorLinesControllers[conveyorLineControllerIndex].AddLine();
        }

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
            if (instance != null)
            {
                Debug.LogError($"{nameof(ConveyorController)}: Another ConveyorController was found!");
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        protected virtual IEnumerator AddLineWithAwait(int conveyorLineControllerIndex)
        {
            AddingLineIndex = conveyorLineControllerIndex;
            NotifyOnLineAddStart();
            MoveLines(conveyorLineControllerIndex, conveyorLineControllerIndex + 1);
            conveyorLinesControllers[conveyorLineControllerIndex].AddLine();
            if (awaitTimeOnLineAdd)
            {
                yield return new WaitForSeconds(addLineTimeAwait);
            }
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

        protected virtual void NotfityOnDataChange() => onDataChange();

        protected virtual void NotifyOnLineAddStart() => onLineAddStart();

        protected virtual void NotifyOnLineAddEnd() => onLineAddEnd();

        protected virtual void OnDisable()
        {
            if (addLineCoroutine != null)
            {
                StopCoroutine(addLineCoroutine);
            }
        }
    }
}