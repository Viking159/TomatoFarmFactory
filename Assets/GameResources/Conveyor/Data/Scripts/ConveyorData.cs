namespace Features.Conveyor.Data
{
    using Features.Extensions.Data.UpdateableParam;
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Conveyor settings data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ConveyorData), menuName = "Features/Data/Conveyor/" + nameof(ConveyorData))]
    public class ConveyorData : StoreableSO
    {
        /// <summary>
        /// Conveyor speed
        /// </summary>
        public virtual float Speed => speed.GetGrowthValue(level);
        [SerializeField]
        protected FloatUpdateableParam speed = new FloatUpdateableParam()
        {
            ParamValue = 1.67f,
            Ratio = 0.1999f
        };
    }
}