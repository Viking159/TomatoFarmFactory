namespace Features.SpawnObjectSpriteLevelController
{
    using Features.Data;
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// SpawnObjectSpriteLevelController
    /// </summary>
    public class SpawnObjectSpriteLevelController : MonoBehaviour
    {
        [SerializeField]
        protected AbstractSpawnObject spawnObject = default;
        [SerializeField]
        protected SpritesData spriteLevelData = default;

        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake()
            => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable() => SetView();

        protected virtual void SetView()
        {
            if (spawnObject.Level < spriteLevelData.SpritesCount)
            {
                spriteRenderer.sprite = spriteLevelData.GetSpriteByIndex(spawnObject.Level);
            }
        }
    }
}