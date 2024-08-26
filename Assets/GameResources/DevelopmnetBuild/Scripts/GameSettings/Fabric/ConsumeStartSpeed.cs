namespace Features.ConsumeStartSpeed
{
    using Features.AbstractFloatStart;
    using Features.Fabric.Data;

    public class ConsumeStartSpeed : AbstractFloatSet<FabricData>
    {
        protected override float paramValue => data.ConsumeSpeed.ParamValue;

        protected override void SetData(float res)
        {
            data.SetConsumeStartCount(res);
        }
    }
}