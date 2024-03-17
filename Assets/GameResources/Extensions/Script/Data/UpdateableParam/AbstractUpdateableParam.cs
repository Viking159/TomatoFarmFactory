namespace Features.Extensions.Data.UpdateableParam
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
        [Range(-10, 10)]
        public float Ratio = 0.3f;

        protected const int DEFAULT_VALUE = 1;

        /// <summary>
        /// Get value by level
        /// </summary>
        public abstract T GetGrowthValue(int level);
    }
}