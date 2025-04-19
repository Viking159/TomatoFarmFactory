namespace Features.InfoBox
{
    using UnityEngine;
    using Features.Spawner;

    /// <summary>
    /// Ferm info box controller
    /// </summary>
    public class CreatorInfoBoxController : AbstractInfoBoxController
    {
        [SerializeField]
        protected AbstractObjectCreator creator = default;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (infoBoxContainer != null && infoBoxContainer.InfoBoxPrefab is not BaseCreatorInfoBox)
            {
                Debug.LogError($"{nameof(CreatorInfoBoxController)}: infoBox is not BaseCreatorInfoBox");
            }
        }
#endif

        protected override void SpawnInfoBox()
        {
            if (infoBoxContainer.InfoBox == null)
            {
                infoBoxContainer.Init(Instantiate(infoBoxContainer.InfoBoxPrefab));
            }
            else
            {
                StopListenInfoBox();
            }
            infoBoxContainer.InfoBox.gameObject.SetActive(true);
            (infoBoxContainer.InfoBox as BaseCreatorInfoBox).InitData(creator, creator.SpawnerData);
            infoBoxContainer.InfoBox.onBoxClose += CloseInfoBox;
        }

        protected virtual void CloseInfoBox()
        {
            StopListenInfoBox();
            if (infoBoxContainer.InfoBox != null)
            {
                infoBoxContainer.InfoBox.gameObject.SetActive(false);
            }
        }

        protected virtual void StopListenInfoBox()
        {
            if (infoBoxContainer.InfoBox != null)
            {
                infoBoxContainer.InfoBox.onBoxClose -= CloseInfoBox;
            }
        }

        protected virtual void OnDisable()
            => StopListenInfoBox();
    }
}

