namespace Features.Ferm
{
    using Features.Data;
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// Sprite view of object with rang
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class RangSpriteView : MonoBehaviour
    {
        [SerializeField]
        protected AbstractObjectCreator creator = default;
        [SerializeField]
        protected SpritesData spritesData = default;
        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake()
            => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable()
        {
            SetSprite();
            creator.onDataChange += SetSprite;
        }

        protected virtual void SetSprite()
            => spriteRenderer.sprite = spritesData.GetSpriteByIndex(creator.Rang);

        protected virtual void OnDisable()
            => creator.onDataChange -= SetSprite;
    }
}