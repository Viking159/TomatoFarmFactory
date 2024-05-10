namespace Features.Spawner
{
    using Features.Data;
    using UnityEngine;
    using Features.Extensions.Data.UpdateableParam;

    /// <summary>
    /// Spawner data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SpawnerData), menuName = "Features/Spawner/" + nameof(SpawnerData))]
    public class SpawnerData : DoubleStoreableSO
    {
        /// <summary>
        /// Speed
        /// </summary>
        public float Speed => speed.GetGrowthValue(level);

        [SerializeField]
        protected FloatUpdateableParam speed = new FloatUpdateableParam()
        {
            ParamValue = 1.67f,
            Ratio = 0.1999f
        };
    }
}

