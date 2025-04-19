namespace Features.InfoBox
{
    using Features.Data;
    using UnityEngine;

    public class ConveyorUpdateButtonView : MonoBehaviour
    {
        [SerializeField]
        protected ConveyorInfoBox infoBox = default;
        [SerializeField]
        protected StoreableSO data = default;
        [SerializeField]
        protected GameObject updateLevelButton = default;

        protected virtual void OnEnable()
        {
            SetView();
            infoBox.onDataChange += SetView;
        }

        protected virtual void SetView() 
            => updateLevelButton.SetActive(infoBox.ConveyorController != null && infoBox.ConveyorController.Level < data.MaxLevel);

        protected virtual void OnDisable()
            => infoBox.onDataChange -= SetView;
    }
}