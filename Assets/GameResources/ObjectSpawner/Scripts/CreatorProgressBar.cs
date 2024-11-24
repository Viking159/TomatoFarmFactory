namespace Features.Spawner
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Ferm progress bar controller
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class CreatorProgressBar : MonoBehaviour
    {
        [SerializeField]
        protected AbstractObjectCreator creator = default;

        protected Slider slider = default;

        protected virtual void Awake()
            => slider = GetComponent<Slider>();

        protected virtual void OnEnable()
        {
            UpdateProgressBar();
            if (creator != null)
            {
                creator.onProgressValueChange += UpdateProgressBar;
            }
        }

        protected virtual void UpdateProgressBar()
            => slider.value = creator.Progress;

        protected virtual void OnDisable()
        {
            if (creator != null)
            {
                creator.onProgressValueChange -= UpdateProgressBar;
            }
        }
    }
}