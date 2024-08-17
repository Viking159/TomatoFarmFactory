namespace Features.Conveyor
{
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
        /// Add spawner to list event
        /// </summary>
        public event Action onSpawnerAdd = delegate { };

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
        /// Line height
        /// </summary>
        public float Height => height;
        [SerializeField, Min(0)]
        protected float height = 1f;

        protected AbstractObjectCreator abstractObjectCreator = default;

        /// <summary>
        /// Add spanwer in spawners list
        /// </summary>
        /// <param name="spawnerObject"></param>
        public virtual void AddSpawner(GameObject spawnerObject)
        {
            if (spawnerObject.TryGetComponent(out abstractObjectCreator))
            {
                spawners.Add(abstractObjectCreator);
                NotifyOnSpawnerAdd();
                return;
            }
            Debug.LogError($"{nameof(ConveyorLineController)}: AbstractObjectCreator not found");
        }

        protected virtual void NotifyOnSpawnerAdd() => onSpawnerAdd();
    }
}

