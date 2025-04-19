namespace Features.InfoBox
{
    using UnityEngine;
    using Features.Conveyor;

    public class ConveyorInfoBoxController : AbstractInfoBoxController
    {
        [SerializeField]
        protected ConveyorElement conveyorElement = default;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (infoBoxContainer != null && infoBoxContainer.InfoBoxPrefab is not ConveyorInfoBox)
            {
                Debug.LogError($"{nameof(ConveyorInfoBoxController)}: infoBox is not ConveyorInfoBox");
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
            (infoBoxContainer.InfoBox as ConveyorInfoBox).InitData(conveyorElement.ConveyorController);
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

