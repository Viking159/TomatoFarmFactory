namespace Features.InfoBox
{
    using Features.Data;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Level progress slider controller
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class ConveyorLevelProgress : MonoBehaviour
    {
        [SerializeField]
        protected ConveyorInfoBox infoBox = default;
        [SerializeField]
        protected StoreableSO data = default;
        protected Slider slider = default;

        protected virtual void Awake()
            => slider = GetComponent<Slider>();

        protected virtual void OnEnable()
        {
            SetSlider();
            infoBox.onDataChange += SetSlider;
        }

        protected virtual void SetSlider()
            => slider.value = (infoBox.ConveyorController == null ? 0 : infoBox.ConveyorController.Level) / (float)data.MaxLevel;

        protected virtual void OnDisable()
            => infoBox.onDataChange -= SetSlider;
    }
}