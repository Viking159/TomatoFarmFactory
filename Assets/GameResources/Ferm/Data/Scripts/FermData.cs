namespace Features.Ferm.Data
{
    using Features.Data;
    using UnityEngine;
    using Features.Extensions.Data.UpdateableParam;

    /// <summary>
    /// Ferm data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FermData), menuName = "Features/Data/Ferm/" + nameof(FermData))]
    public class FermData : DoubleStoreableSO
    {
        /// <summary>
        /// Ferm name
        /// </summary>
        public string Name => fermName;
        [SerializeField]
        private string fermName = string.Empty;

        /// <summary>
        /// Speed
        /// </summary>
        public float Speed => speed.GetGrowthValue(level);

        [SerializeField]
        protected FloatUpdateableParam speed = new FloatUpdateableParam()
        {
            ParamValue = 2,
            GrowthPercent = 50
        };
    }
}