namespace Features.DoubleStoreableSpriteView
{
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Sprite view of object with rang
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class RangSpriteView : MonoBehaviour
    {
        [SerializeField]
        protected DoubleStoreableSO data = default;
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
            => spriteRenderer.sprite = spritesData.GetSpriteByLevel(data.Rang);

        protected virtual void OnDisable()
            => data.onDataChange -= SetSprite;
    }
}