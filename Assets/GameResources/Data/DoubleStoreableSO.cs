namespace Features.Data
{
    using Features.Extensions.Data.UpdateableParam;
    using UnityEngine;

    /// <summary>
    /// Abstract class for storeable SO with two storeable datas
    /// </summary>
    public abstract class DoubleStoreableSO : StoreableSO
    {
        /// <summary>
        /// Max rang
        /// </summary>
        public int MaxRang => maxRang;
        [SerializeField]
        private int maxRang = 5;

        [SerializeField]
        protected IntUpdateableParam updateRangPrice = new IntUpdateableParam
        {
            ParamValue = 1,
            Ratio = 0.1f
        };

        /// <summary>
        /// Update rang price
        /// </summary>
        public int GetUpdateRangPrice(int rang) => updateLevelPrice.GetGrowthValue(rang);
    }
}