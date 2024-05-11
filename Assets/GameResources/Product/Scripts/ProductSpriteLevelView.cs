namespace Features.Product
{
    using Features.Product.Data;
    using Features.Spawner.View;

    /// <summary>
    /// Product sprite view
    /// </summary>
    public class ProductSpriteLevelView : AbstractSpiteView<ProductData>
    {
        protected override int GetSpriteIndex()
            => objectController.Data.Level;
    }
}