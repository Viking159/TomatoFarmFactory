namespace Features.InfoBox
{
    using UnityEngine;
    using UnityEngine.EventSystems;
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

        protected static BaseCreatorInfoBox infoBox = default;

        protected virtual void OnMouseUp()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SpawnInfoBox();
            }
        }

        protected virtual void SpawnInfoBox()
        {
            if (infoBox == null)
            {
                infoBox = Instantiate(infoBoxPrefab);
            }
            else
            {
                StopListenInfoBox();
            }
            infoBox.gameObject.SetActive(true);
            infoBox.InitData(creator, creator.SpawnerData);
            infoBox.onBoxClose += CloseInfoBox;
        }

        protected virtual void CloseInfoBox()
        {
            StopListenInfoBox();
            if (infoBox != null)
            {
                infoBox.gameObject.SetActive(false);
            }
        }

        protected virtual void StopListenInfoBox()
        {
            if (infoBox != null)
            {
                infoBox.onBoxClose -= CloseInfoBox;
            }
        }

        protected virtual void OnDisable()
            => StopListenInfoBox();
    }
}

