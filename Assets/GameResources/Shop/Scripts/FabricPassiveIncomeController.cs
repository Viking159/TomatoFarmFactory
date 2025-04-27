namespace Features.Shop
{
    using Features.Product.Data;
    using UnityEngine;

    public class FabricPassiveIncomeController : AbstractSpawnerPassiveIncomeController
    {
        [SerializeField]
        protected ProductData productData = default;

        protected override int GetCount(int level) => productData.GetFruitsCount(level);

        protected override int GetPrice(int level) => productData.GetPrice(level);
    }
}