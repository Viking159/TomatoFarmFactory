namespace Features.Spawner.View
{
    using Features.Data;
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// Abstract sprite view of AbstractInitableObject
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class AbstractSpiteView<T> : MonoBehaviour
        where T: StoreableSOWithSprites
    {
        [SerializeField]
        protected AbstractInitableObject<T> objectController = default;

        protected SpriteRenderer spriteRenderer = default;

        /// <summary>
        /// Get sprite index
        /// </summary>
        protected abstract int GetSpriteIndex();

        protected virtual void Awake() 
            => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable()
        {
            SetView();
            objectController.onDataInited += SetView;
        }

        protected virtual void SetView() 
            => spriteRenderer.sprite = objectController.Data != null 
            ? objectController.Data.SpritesData.GetSpriteByIndex(GetSpriteIndex())
            : default;

        protected virtual void OnDisable() 
            => objectController.onDataInited -= SetView;
    }
}