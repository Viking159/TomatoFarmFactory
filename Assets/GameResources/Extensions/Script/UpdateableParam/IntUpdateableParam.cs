using System;

namespace Extensions.UpdateableParam
{
    /// <summary>
    /// Int updateable param
    /// </summary>
    [Serializable]
    public class IntUpdateableParam : AbstractUpdateableParam<int>
    {
        public override int GetGrowthValue(int level)
            => (int)(ParamValue * MathF.Pow(PercentToFloat(), level));
    }
}