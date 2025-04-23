namespace Features.Store
{
    public class NonConsumableSwitcher : AbstractNonCunsumableSwitcher
    {
        protected override bool SwitchCondition() => UIAPStore.Instance.IsAvailableToPurchase(productId) && !UIAPStore.Instance.IsBought(productId);
    }
}