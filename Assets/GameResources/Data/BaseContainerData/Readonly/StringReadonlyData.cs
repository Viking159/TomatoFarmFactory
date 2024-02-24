namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// String readonly data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(StringReadonlyData), menuName = "Features/Data/BaseContainerData/" + nameof(StringReadonlyData))]
    public sealed class StringReadonlyData : AbstractReadonlyData<string> {}
}