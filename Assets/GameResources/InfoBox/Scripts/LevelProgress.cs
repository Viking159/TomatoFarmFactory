namespace Features.InfoBox
{
    using Features.Data;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Level progress slider controller
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected StoreableSO data = default;
        protected Slider slider = default;

        protected virtual void Awake()
            => slider = GetComponent<Slider>();

        protected virtual void OnEnable()
        {
            SetSlider();
            baseCreatorInfoBox.onDataChange += SetSlider;
        }

        protected virtual void SetSlider()
            => slider.value = baseCreatorInfoBox.SpawnerData.Level / (float)data.MaxLevel;

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetSlider;
    }
}