namespace Features.InfoBox
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Features.InfoBox;

    /// <summary>
    /// Ferm info box controller
    /// </summary>
    public class InfoBoxController : MonoBehaviour
    {
        [SerializeField]
        protected InfoBox infoBoxPrefab = default;

        protected InfoBox infoBox = default;

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

