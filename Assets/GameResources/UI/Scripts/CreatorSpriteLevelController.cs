namespace Features.UI
{
    using Features.Data;
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// Sprite controller based in level data
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class CreatorSpriteLevelController : MonoBehaviour
    {
        [SerializeField]
        protected AbstractObjectCreator creator = default;
        [SerializeField]
        protected SpritesData spriteLevelData = default;

        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake()
            => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable()
        {
            SetView();
            creator.onDataChange += SetView;
        }

        protected virtual void SetView()
        {
            if (creator.Level < spriteLevelData.SpritesCount)
            {
                spriteRenderer.sprite = spriteLevelData.GetSpriteByIndex(creator.Level);
            }
        }

        protected virtual void OnDisable()
            => creator.onDataChange -= SetView;
    }
}