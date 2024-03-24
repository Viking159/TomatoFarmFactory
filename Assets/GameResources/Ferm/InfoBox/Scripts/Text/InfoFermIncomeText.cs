namespace Features.Ferm.InfoBox
{
    using Features.Fruit.Data;
    using UnityEngine;

    /// <summary>
    /// Ferm income info text
    /// </summary>
    public class InfoFermIncomeText : AbstractInfoFermText
    {
        [SerializeField]
        protected FruitData fruitData = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            fruitData.onDataChange += SetText;
        }

        protected override void SetText()
            => SetView(string.Format(mask, fruitData.Price * fruitData.Count));

        protected override void OnDisable()
        {
            base.OnDisable();
            fruitData.onDataChange -= SetText;
        }
    }
}

