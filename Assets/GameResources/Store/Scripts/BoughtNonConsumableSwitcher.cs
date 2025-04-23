namespace Features.Store
{
    public class BoughtNonConsumableSwitcher : AbstractNonCunsumableSwitcher
    {
        protected override bool SwitchCondition() => !UIAPStore.Instance.IsBought(productId);
    }
}