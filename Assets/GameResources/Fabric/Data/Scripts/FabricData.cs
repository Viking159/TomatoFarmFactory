namespace Features.Fabric.Data
{
    using Features.Extensions.Data.UpdateableParam;
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// Fabric data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FabricData), menuName = "Features/Fabric/Data/" + nameof(FabricData))]
    public class FabricData : SpawnerData
    {
        [SerializeField]
        protected FloatUpdateableParam consumeSpeed = new FloatUpdateableParam()
        {
            ParamValue = 1.67f,
            Ratio = 0.1999f
        };

        /// <summary>
        /// Fruits consuming speed
        /// </summary>
        public float GetConsumeSpeed(int rang) => consumeSpeed.GetGrowthValue(rang);
    }
}