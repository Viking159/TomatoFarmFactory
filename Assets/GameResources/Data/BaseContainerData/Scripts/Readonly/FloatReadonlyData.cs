namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// Float readonly data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FloatReadonlyData), menuName = "Features/Data/BaseContainerData/" + nameof(FloatReadonlyData))]
    public sealed class FloatReadonlyData : AbstractReadonlyData<float> {}
}