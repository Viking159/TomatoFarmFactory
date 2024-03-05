namespace Features.Fruit
{
    using Features.Data;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Base fruit view
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class BaseFruitView : MonoBehaviour
    {
        [SerializeField]
        protected BaseFruit baseFruit = default;
        [SerializeField]
        protected SpriteLevelData spriteLevelData = default;

        protected Image image = default;

        protected virtual void Awake()
        {
            image = GetComponent<Image>();
            baseFruit.onDataInited += SetView;
            SetView();
        }

        protected virtual void SetView()
        {
            if (baseFruit.Level < spriteLevelData.SpritesCount)
            {
                image.sprite = spriteLevelData.GetSpriteByLevel(baseFruit.Level);
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