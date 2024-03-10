namespace Features.Conveyor
{
    using UnityEngine;
    using Features.Conveyor.Data;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Conveyor controller
    /// </summary>
    public class ConveyorController : MonoBehaviour
    {
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

        [SerializeField]
        protected ConveyorData conveyorData = default;
        [SerializeField]
        protected List<ConveyorLine> conveyorLines = new List<ConveyorLine>();

        protected virtual void Awake()
        {
            conveyorData.onDataChange += Notfity;
            InitLines();
        }

        protected virtual void InitLines()
        {
            for (int i = 0; i <= conveyorData.Level; i++)
            {
                foreach (ConveyorElement conveyorElement in conveyorLines[i].conveyorElements)
                {
                    conveyorElement.gameObject.SetActive(true);
                    conveyorElement.Init(this);
                }
            }
        }

        protected virtual void Notfity()
            => onLevelChange();

        protected virtual void OnDestroy()
            => conveyorData.onDataChange -= Notfity;
    }
}