using System;

namespace Extensions.UpdateableParam
{
    /// <summary>
    /// Float updateable param
    /// </summary>
    [Serializable]
    public class FloatUpdateableParam : AbstractUpdateableParam<float>
    {
        public override float GetGrowthValue(int level)
            => ParamValue * MathF.Pow(PercentToFloat(), level);
    }
}