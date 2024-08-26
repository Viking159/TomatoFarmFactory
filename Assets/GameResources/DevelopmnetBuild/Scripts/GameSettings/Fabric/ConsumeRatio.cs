namespace Features.ConsumeRatio
{
    using Features.AbstractFloatStart;
    using Features.Fabric.Data;

    public class ConsumeRatio : AbstractFloatSet<FabricData>
    {
        protected override float paramValue => data.ConsumeSpeed.Ratio;

        protected override void SetData(float res)
        {
            data.SetConsumeRatio(res);
        }
    }
}