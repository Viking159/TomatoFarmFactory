namespace Features.LevelProgress
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
        protected StoreableSO data = default;
        protected Slider slider = default;

        protected virtual void Awake()
            => slider = GetComponent<Slider>();

        protected virtual void OnEnable()
        {
            SetSlider();
            data.onDataChange += SetSlider;
        }

        protected virtual void SetSlider()
            => slider.value = data.Level / (float)data.MaxLevel;

        protected virtual void OnDisable()
            => data.onDataChange -= SetSlider;
    }
}