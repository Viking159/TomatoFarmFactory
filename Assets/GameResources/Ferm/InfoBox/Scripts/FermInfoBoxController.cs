namespace Features.Ferm.InfoBox
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Ferm info box controller
    /// </summary>
    public class FermInfoBoxController : MonoBehaviour
    {
        [SerializeField]
        protected InfoBox infoBoxPrefab = default;
        [SerializeField]
        protected Transform infoBoxParent = default;

        protected InfoBox infoBox = default;

        protected const int DEFAULT_HITS_COUNT = 10;

        private void OnMouseUp()
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
            infoBox = Instantiate(infoBoxPrefab, infoBoxParent);
            infoBox.onBoxClose += DestroyInfoBox;
        }

        protected virtual void DestroyInfoBox()
        {
            if (infoBox != null)
            {
                infoBox.onBoxClose -= DestroyInfoBox;
                infoBox.gameObject.SetActive(false);
                Destroy(infoBox.gameObject);
            }
        }    
    }
}

