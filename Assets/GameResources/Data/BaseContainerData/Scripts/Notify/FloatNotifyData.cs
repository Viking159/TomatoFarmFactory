namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// Float notify data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FloatNotifyData), menuName = "Features/Data/BaseContainerData/" + nameof(FloatNotifyData))]
    public sealed class FloatNotifyData : AbstractNotifyData<float> {}
}