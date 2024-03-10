namespace Features.Ferm.InfoBox
{
    using Features.Ferm.Data;
    using UnityEngine;

    /// <summary>
    /// Update buttons view controller
    /// </summary>
    public class UpdateButtonView : MonoBehaviour
    {
        [SerializeField]
        protected FermDataContainer fermDataContainer = default;
        [SerializeField]
        protected GameObject updateLevelButton = default;
        [SerializeField]
        protected GameObject updateRangButton = default;

        protected virtual void OnEnable()
        {
            SetView();
            fermDataContainer.onDataChange += SetView;
        }

        protected virtual void SetView()
        {
            DisableButtons();
            if (fermDataContainer.Level < fermDataContainer.MaxLevel)
            {
                updateLevelButton.SetActive(true);
                return;
            }
            if (fermDataContainer.Rang < fermDataContainer.MaxRang)
            {
                updateRangButton.SetActive(true);
            }
        }

        protected virtual void DisableButtons()
        {
            updateLevelButton.SetActive(false);
            updateRangButton.SetActive(false);
        }

        protected virtual void OnDisable()
            => fermDataContainer.onDataChange -= SetView;
    }
}