namespace Features.CreatorStartSpeed
{
    using Features.AbstractFloatStart;
    using Features.Spawner;

    public class CreatorStartSpeed : AbstractFloatSet<SpawnerData>
    {
        protected override float paramValue => data.Speed.ParamValue;

        protected override void SetData(float res)
        {
            data.SetSpawnStartSpeed(res);
        }
    }
}