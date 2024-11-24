namespace Features.Fruit
{
    using UnityEngine;
    using Features.Fruit.Data;
    using Features.Spawner.View;
    using Features.Spawner;

    /// <summary>
    /// Fruit sprite view
    /// </summary>
    public class FruitSpriteLevelView : AbstractSpiteView<FruitData>
    {
        protected override int GetSpriteIndex()
            => objectController.Level;
    }
}