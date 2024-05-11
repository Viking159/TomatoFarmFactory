namespace Features.UI
{
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Sprite controller based in level data
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteLevelController : MonoBehaviour
    {
        [SerializeField]
        protected StoreableSO levelData = default;
        [SerializeField]
        protected SpritesData spriteLevelData = default;

        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake()
            => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable()
        {
            SetView();
            levelData.onDataChange += SetView;
        }

        protected virtual void SetView()
        {
            if (levelData.Level < spriteLevelData.SpritesCount)
            {
                spriteRenderer.sprite = spriteLevelData.GetSpriteByIndex(levelData.Level);
            }
        }

        protected virtual void OnDisable()
            => levelData.onDataChange -= SetView;
    }
}