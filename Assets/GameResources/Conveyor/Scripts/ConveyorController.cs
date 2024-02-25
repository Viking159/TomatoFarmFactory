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
        public int Level => _level;
        private int _level = 0;

        /// <summary>
        /// Conveyor speed
        /// </summary>
        public float Speed => _speed;
        private float _speed = 0;

        [SerializeField]
        private ConveyorData _conveyorData = default;
        [SerializeField]
        private List<ConveyorLine> _conveyorLines = new List<ConveyorLine>();

        private void Awake()
        {
            LoadData();
            InitData();
            InitLines();
        }

        private void LoadData()
        {
            //TODO: load data
        }

        private void InitData()
        {
            _speed = _conveyorData.DefaultSpeed * MathF.Pow((1 + _conveyorData.IncreasePercentage / 100f), _level);
        }

        private void InitLines()
        {
            for (int i = 0; i <= _level; i++)
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