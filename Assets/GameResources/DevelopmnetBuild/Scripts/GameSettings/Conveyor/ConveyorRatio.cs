namespace Features.ConveyorRatio
{
    using Features.AbstractFloatStart;
    using Features.Conveyor.Data;

    public class ConveyorRatio : AbstractFloatSet<ConveyorData>
    {
        protected override float paramValue => data.Speed.Ratio;

        protected override void SetData(float res)
        {
            data.SetSpeedRatio(res);
        }
    }
}