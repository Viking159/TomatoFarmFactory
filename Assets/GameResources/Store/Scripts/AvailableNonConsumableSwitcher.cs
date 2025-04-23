namespace Features.Store
{
    public class AvailableNonConsumableSwitcher : AbstractNonCunsumableSwitcher
    {
        protected override bool SwitchCondition() => UIAPStore.Instance.IsAvailableToPurchase(productId);
    }
}