namespace Features.Conveyor
{
    using UnityEngine;
    using Features.Conveyor.Data;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Conveyor controller
    /// </summary>
    public sealed class ConveyorController : MonoBehaviour
    {
        /// <summary>
        /// Conveyor level change event
        /// </summary>
        public event Action onLevelChange = delegate { };

        /// <summary>
        /// Conveyor level
        /// </summary>
        public int Level => _conveyorData.Level;

        /// <summary>
        /// Conveyor speed
        /// </summary>
        public float Speed => _conveyorData.Speed;

        [SerializeField]
        private ConveyorData _conveyorData = default;
        [SerializeField]
        private List<ConveyorLine> _conveyorLines = new List<ConveyorLine>();

        private void Awake()
        {
            InitLines();
        }

        private void InitLines()
        {
            for (int i = 0; i <= _conveyorData.Level; i++)
            {
                foreach (ConveyorElement conveyorElement in _conveyorLines[i].conveyorElements)
                {
                    conveyorElement.gameObject.SetActive(true);
                    conveyorElement.Init(this);
                }
            }
        }
    }
}