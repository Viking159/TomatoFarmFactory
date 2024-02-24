namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// Int notify data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(IntNotifyData), menuName = "Features/Data/BaseContainerData/" + nameof(IntNotifyData))]
    public sealed class IntNotifyData : AbstractNotifyData<int> {}
}