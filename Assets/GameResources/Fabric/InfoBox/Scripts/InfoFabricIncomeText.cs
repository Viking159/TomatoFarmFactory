namespace Features.Fabric.InfoBox
{
    using Features.Extensions.View;
    using Features.InfoBox;
    using Features.Product.Data;
    using UnityEngine;

    /// <summary>
    ///  Fabric income info text
    /// </summary>
    public class InfoFabricIncomeText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected ProductData productData = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(string.Format(mask, productData.GetPrice(0) * productData.GetFruitsCount(baseCreatorInfoBox.SpawnerData.Rang)));

        protected virtual void Ondisable() => baseCreatorInfoBox.onDataChange -= SetText;
    }
}