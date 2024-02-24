namespace Features.Conveyor.Data
{
    using UnityEngine;

    /// <summary>
    /// Conveyor settings data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ConveyorData), menuName = "Features/Data/Conveyor/" + nameof(ConveyorData))]
    public sealed class ConveyorData : ScriptableObject
    {
        /// <summary>
        /// Default conveyor speed
        /// </summary>
        public float DefaultSpeed => _defaultSpeed;
        [SerializeField]
        private float _defaultSpeed = 10;

        /// <summary>
        /// Speed increase per level percentage
        /// </summary>
        public float IncreasePercentage => _increasePercentage;
        [SerializeField]
        private float _increasePercentage = 10;
    }
}