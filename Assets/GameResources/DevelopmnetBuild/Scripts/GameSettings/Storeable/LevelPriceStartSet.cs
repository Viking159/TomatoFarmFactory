namespace Features.StartLevelSet
{
    using Features.AbstractFloatRatio;
    using Features.Data;

    public class LevelPriceStartSet : AbstractIntSet<StoreableSO>
    {
        protected override int paramValue => data.LevelPrice.ParamValue;

        protected override void SetData(int res)
        {
            data.SetStartLevelPrice(res);
        }
    }
}