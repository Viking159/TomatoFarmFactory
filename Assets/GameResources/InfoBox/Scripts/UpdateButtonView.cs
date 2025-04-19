namespace Features.InfoBox
{
    using Features.Data;
    using UnityEngine;

    /// <summary>
    /// Update buttons view controller
    /// </summary>
    public class UpdateButtonView : MonoBehaviour
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected DoubleStoreableSO data = default;
        [SerializeField]
        protected GameObject updateLevelButton = default;
        [SerializeField]
        protected GameObject updateRangButton = default;

        protected virtual void OnEnable()
        {
            SetView();
            baseCreatorInfoBox.onDataChange += SetView;
        }

        protected virtual void SetView()
        {
            DisableButtons();
            if (baseCreatorInfoBox.SpawnerData != null)
            {
                if (baseCreatorInfoBox.SpawnerData.Level < data.MaxLevel)
                {
                    updateLevelButton.SetActive(true);
                    return;
                }
                if (baseCreatorInfoBox.SpawnerData.Rang < data.MaxRang)
                {
                    updateRangButton.SetActive(true);
                }
            }
        }

        protected virtual void DisableButtons()
        {
            updateLevelButton.SetActive(false);
            updateRangButton.SetActive(false);
        }

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetView;
    }
}