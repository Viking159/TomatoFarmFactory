namespace Features.InfoBox
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Features.InfoBox;
    using Features.Spawner;

    /// <summary>
    /// Ferm info box controller
    /// </summary>
    public class InfoBoxController : MonoBehaviour
    {
        [SerializeField]
        protected AbstractObjectCreator creator = default;
        [SerializeField]
        protected BaseCreatorInfoBox infoBoxPrefab = default;

        protected BaseCreatorInfoBox infoBox = default;

        protected virtual void OnMouseUp()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SpawnInfoBox();
                return;
            }
        }

        protected virtual void SpawnInfoBox()
        {
            DestroyInfoBox();
            infoBox = Instantiate(infoBoxPrefab);
            infoBox.InitData(creator, creator.SpawnerData);
            infoBox.onBoxClose += DestroyInfoBox;
        }

        protected virtual void DestroyInfoBox()
        {
            if (infoBox != null)
            {
                infoBox.onBoxClose -= DestroyInfoBox;
                Destroy(infoBox.gameObject);
            }
        }

        protected virtual void OnDestroy()
            => DestroyInfoBox();
    }
}

