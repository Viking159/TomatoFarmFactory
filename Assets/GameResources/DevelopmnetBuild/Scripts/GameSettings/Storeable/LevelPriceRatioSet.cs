namespace Features.LevelPriceRatioSet
{
    using Features.AbstractFloatStart;
    using Features.Data;

    public class LevelPriceRatioSet : AbstractFloatSet<StoreableSO>
    {
        protected override float paramValue => data.LevelPrice.Ratio;

        protected override void SetData(float res)
        {
            data.SetStartLevelRatio(res);
        }
    }
}