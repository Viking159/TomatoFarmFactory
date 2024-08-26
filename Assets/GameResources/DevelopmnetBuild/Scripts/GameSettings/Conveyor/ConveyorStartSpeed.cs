namespace Features.ConveyorStartSpeed
{
    using Features.AbstractFloatStart;
    using Features.Conveyor.Data;

    public class ConveyorStartSpeed : AbstractFloatSet<ConveyorData>
    {
        protected override float paramValue => data.Speed.ParamValue;

        protected override void SetData(float res)
        {
            data.SetStartSpeed(res);
        }
    }
}