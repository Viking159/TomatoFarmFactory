namespace Features.Ferm.InfoBox
{
    using Features.Data;
    using Features.Fruit.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Ferm income info text
    /// </summary>
    public class InfoFermIncomeText : AbstractInfoFermText
    {
        [SerializeField]
        protected FruitData fruitData = default;
        [SerializeField]
        protected int digitsCount = 2;

        protected override void OnEnable()
        {
            base.OnEnable();
            fruitData.onDataChange += SetText;
        }

        protected override void SetText()
            => SetView(string.Format(mask, Math.Round(fruitData.Price * fruitData.Count * fermDataContainer.Speed / GlobalData.SPEED_CONVERT_RATIO, digitsCount)));

        protected override void OnDisable()
        {
            base.OnDisable();
            fruitData.onDataChange -= SetText;
        }
    }
}

