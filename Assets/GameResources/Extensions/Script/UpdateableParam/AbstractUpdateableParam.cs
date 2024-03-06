namespace Features.Extensions.UpdateableParam
{
    using UnityEngine;

    /// <summary>
    /// Abstract updateable param 
    /// </summary>
    public abstract class AbstractUpdateableParam<T> where T: struct
    {
        /// <summary>
        /// Value
        /// </summary>
        public T ParamValue = default;

        /// <summary>
        /// Growth percent value
        /// </summary>
        [Min(0)]
        public int GrowthPercent = 50;

        protected const int DEFAULT_VALUE = 1;
        protected const int FULL_PERCENT_VALUE = 100;

        /// <summary>
        /// Get value by level
        /// </summary>
        public abstract T GetGrowthValue(int level);

        protected virtual float PercentToFloat()
            => DEFAULT_VALUE + GrowthPercent / (float)FULL_PERCENT_VALUE;
    }
}