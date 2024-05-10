namespace Features.StoreableSpriteView
{
    /// <summary>
    /// Fruit sprite view
    /// </summary>
    public class FruitSpriteLevelView : LevelSpriteView
    {
        protected override void OnEnable() 
            => SetSprite();

        protected override void OnDisable() { }
    }
}

