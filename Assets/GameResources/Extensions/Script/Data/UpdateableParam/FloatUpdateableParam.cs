namespace Features.Extensions.Data.UpdateableParam
{
    using System;

    /// <summary>
    /// Float updateable param
    /// </summary>
    [Serializable]
    public class FloatUpdateableParam : AbstractUpdateableParam<float>
    {
        public override float GetGrowthValue(int level)
            => ParamValue * (DEFAULT_VALUE + Ratio * level);
    }
}