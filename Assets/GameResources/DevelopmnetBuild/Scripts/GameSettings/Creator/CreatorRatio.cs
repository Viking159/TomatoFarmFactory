namespace Features.CreatorRatio
{
    using Features.AbstractFloatStart;
    using Features.Spawner;

    public class CreatorRatio : AbstractFloatSet<SpawnerData>
    {
        protected override float paramValue => data.Speed.Ratio;

        protected override void SetData(float res)
        {
            data.SetSpawnRatio(res);
        }
    }
}