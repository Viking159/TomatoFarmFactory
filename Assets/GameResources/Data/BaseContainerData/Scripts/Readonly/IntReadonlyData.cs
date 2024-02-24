namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// Int readonly data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(IntReadonlyData), menuName = "Features/Data/BaseContainerData/" + nameof(IntReadonlyData))]
    public sealed class IntReadonlyData : AbstractReadonlyData<int> {}
}