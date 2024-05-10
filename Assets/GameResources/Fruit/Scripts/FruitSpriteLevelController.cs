namespace Features.StoreableSpriteView
{
    /// <summary>
    /// Fruit sprite view
    /// </summary>
    public class FruitSpriteLevelController : LevelSpriteView
    {
        protected override void OnEnable() 
            => SetSprite();

        protected override void OnDisable() { }
    }
}

