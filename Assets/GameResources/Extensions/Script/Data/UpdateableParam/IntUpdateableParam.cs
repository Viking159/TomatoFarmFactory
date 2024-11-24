namespace Features.Extensions.Data.UpdateableParam
{
    using System;

    /// <summary>
    /// Int updateable param
    /// </summary>
    [Serializable]
    public class IntUpdateableParam : AbstractUpdateableParam<int>
    {
        public override int GetGrowthValue(int level)
            => (int)(ParamValue * (DEFAULT_VALUE + Ratio * level));
    }
}