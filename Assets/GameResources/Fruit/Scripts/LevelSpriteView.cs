namespace Features.StoreableSpriteView
{
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Sprite view of object with level
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelSpriteView : MonoBehaviour
    {
        [SerializeField]
        protected StoreableSO data = default;
        [SerializeField]
        protected SpritesData spritesData = default;
        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake()
            => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable()
        {
            SetSprite();
            data.onDataChange += SetSprite;
        }

        protected virtual void SetSprite()
            => spriteRenderer.sprite = spritesData.GetSpriteByLevel(data.Level);

        protected virtual void OnDisable()
            => data.onDataChange -= SetSprite;
    }
}