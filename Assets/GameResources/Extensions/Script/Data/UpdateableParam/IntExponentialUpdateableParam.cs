namespace Features.Extensions.Data.UpdateableParam
{
    using System;

    /// <summary>
    /// Int exponential updateable param
    /// </summary>
    [Serializable]
    public class IntExponentialUpdateableParam : AbstractUpdateableParam<int>
    {
        public override int GetGrowthValue(int level)
            => (int)(ParamValue * MathF.Pow(Ratio, level));
    }
}