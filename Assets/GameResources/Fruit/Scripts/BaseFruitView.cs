namespace Features.Fruit
{
    using Features.Data;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Base fruit view
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class BaseFruitView : MonoBehaviour
    {
        [SerializeField]
        protected BaseFruit baseFruit = default;
        [SerializeField]
        protected SpriteLevelData spriteLevelData = default;

        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            baseFruit.onDataInited += SetView;
            SetView();
        }

        protected virtual void SetView()
        {
            if (baseFruit.Level < spriteLevelData.SpritesCount)
            {
                spriteRenderer.sprite = spriteLevelData.GetSpriteByLevel(baseFruit.Level);
            }
        }

        protected virtual void OnDestroy()
        {
            if (baseFruit != null)
            {
                baseFruit.onDataInited -= SetView;
            }
        }
    }
}