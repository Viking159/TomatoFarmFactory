namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// String notify data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(StringNotifyData), menuName = "Features/Data/BaseContainerData/" + nameof(StringNotifyData))]
    public sealed class StringNotifyData : AbstractNotifyData<string> {}
}