namespace Features.Ferm
{
    using Features.Ferm.Data;
    using Features.Fruit;
    using Features.Fruit.Data;
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// Ferm controller
    /// </summary>
    public class FermController : AbstractPrefabCreator<BaseFruit, FermData>
    {
        /// <summary>
        /// Fruits creation speed
        /// </summary>
        public override float Speed => data.GetSpeed(Level);

        [SerializeField]
        protected FruitData fruitData = default;

        protected override void InitData()
            => createdObject.InitData(fruitData, Rang);
    }
}