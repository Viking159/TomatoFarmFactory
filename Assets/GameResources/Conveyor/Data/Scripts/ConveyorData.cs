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
        [SerializeField]
        protected FloatUpdateableParam speed = new FloatUpdateableParam()
        {
            ParamValue = 1.67f,
            Ratio = 0.1999f
        };

        /// <summary>
        /// Conveyor speed
        /// </summary>
        public virtual float GetSpeed(int level) => speed.GetGrowthValue(level);
    }
}