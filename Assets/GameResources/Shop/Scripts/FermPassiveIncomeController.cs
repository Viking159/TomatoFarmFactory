namespace Features.Shop
{
    using Features.Fruit.Data;
    using UnityEngine;

    public class FermPassiveIncomeController : AbstractSpawnerPassiveIncomeController
    {
        [SerializeField]
        protected FruitData fruitData = default;
        protected override int GetCount(int level) => fruitData.GetCount(level);

        protected override int GetPrice(int level) => fruitData.GetPrice(level);
    }
}