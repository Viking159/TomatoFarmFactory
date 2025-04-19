namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using Features.Fruit.Data;
    using Features.InfoBox;
    using UnityEngine;

    /// <summary>
    ///  Ferm income info text
    /// </summary>
    public class InfoFermIncomeText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected FruitData fruitData = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(string.Format(mask,
                (
                baseCreatorInfoBox.SpawnerData == null 
                ? 0
                : fruitData.GetPrice(0) * fruitData.GetCount(baseCreatorInfoBox.SpawnerData.Rang))));

        protected virtual void OnDisable() => baseCreatorInfoBox.onDataChange -= SetText;
    }
}
