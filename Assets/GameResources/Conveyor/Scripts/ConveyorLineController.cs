namespace Features.Conveyor
{
    using Features.ConstructPlace;
    using Features.Extensions.BaseDataTypes;
    using Features.SaveSystem;
    using Features.Spawner;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Conveyor line controller
    /// </summary>
    public class ConveyorLineController : MonoBehaviour
    {
        /// <summary>
        /// Init event
        /// </summary>
        public event Action onInit = delegate { };

        /// <summary>
        /// Add spawner to list event
        /// </summary>
        public event Action onSpawnerAdd = delegate { };

        /// <summary>
        /// Is valid controller
        /// </summary>
        public virtual bool ValidController => linesController != null;

        /// <summary>
        /// LineController index in line controllers list
        /// </summary>
        public virtual int Index => ValidController ? linesController.ConveyorLines.IndexOf(this) : -1;

        /// <summary>
        /// Conveyor line
        /// </summary>
        public ConveyorLine ConveyorLine => conveyorLine;
        [SerializeField]
        protected ConveyorLine conveyorLine = default;

        /// <summary>
        /// Count of possible spawners count
        /// </summary>
        public int MaxSpawnersCount => maxSpawnersCount;
        [SerializeField, Min(0)]
        protected int maxSpawnersCount = 0;

        /// <summary>
        /// List of spawners on the line
        /// </summary>
        public IReadOnlyList<AbstractObjectCreator> Spawners => spawners;
        [SerializeField]
        protected List<AbstractObjectCreator> spawners = new List<AbstractObjectCreator>();

        /// <summary>
        /// Conveyor part lines controller
        /// </summary>
        public BaseConveyorLinesController LinesController => linesController;
        protected BaseConveyorLinesController linesController = default;

        /// <summary>
        /// Line controller data
        /// </summary>
        public LineControllerData LineControllerData => lineControllerData;
        protected LineControllerData lineControllerData = default;

        /// <summary>
        /// Line height
        /// </summary>
        public float Height => height;
        [SerializeField, Min(0)]
        protected float height = 1f;

        [SerializeField]
        protected List<ConstructPlaceController> constructPlaceControllers = new List<ConstructPlaceController>();

        protected AbstractObjectCreator abstractObjectCreator = default;

        /// <summary>
        /// Init conveyor line elements
        /// </summary>
        public virtual void InitConveyorLine(BaseConveyorLinesController linesController)
        {
            this.linesController = linesController;
            foreach (ConveyorElement line in conveyorLine.conveyorElements)
            {
                line.Init(this);
            }
            NotifyOnInit();
        }

        /// <summary>
        /// Remove construct place and add spawner
        /// </summary>
        public virtual void AddSpawner(int index)
        {
            if (index >= 0 && index < constructPlaceControllers.Count)
            {
                constructPlaceControllers[index].ConstructPlace();
            }
        }

        /// <summary>
        /// Add spanwer in spawners list
        /// </summary>
        public virtual void AddSpawner(GameObject spawnerObject, int index)
        {
            if (spawnerObject.TryGetComponent(out abstractObjectCreator))
            {
                abstractObjectCreator.InitData(new SaveSystem.SpawnerData() { Index = index });
                spawners.Add(abstractObjectCreator);
                NotifyOnSpawnerAdd();
                return;
            }
            Debug.LogError($"{nameof(ConveyorLineController)}: AbstractObjectCreator not found");
        }

        protected virtual void NotifyOnSpawnerAdd() => onSpawnerAdd();

        protected virtual void NotifyOnInit() => onInit();
    }
}

