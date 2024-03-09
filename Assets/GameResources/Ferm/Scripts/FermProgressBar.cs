namespace Features.Ferm
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Ferm progress bar controller
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class FermProgressBar : MonoBehaviour
    {
        [SerializeField]
        protected FermController fermController = default;

        protected Slider slider = default;

        protected virtual void Awake()
            => slider = GetComponent<Slider>();

        protected virtual void OnEnable()
        {
            UpdateProgressBar();
            if (fermController != null)
            {
                fermController.onProgressValueChange += UpdateProgressBar;
            }
        }

        protected virtual void UpdateProgressBar()
            => slider.value = fermController.Progress;

        protected virtual void OnDisable()
        {
            if (fermController != null)
            {
                fermController.onProgressValueChange -= UpdateProgressBar;
            }
        }
    }
}