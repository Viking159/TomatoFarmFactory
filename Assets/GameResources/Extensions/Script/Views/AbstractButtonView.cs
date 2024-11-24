namespace Features.Extensions.View
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Abstract class for MonoBehaviour with Button component
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView : MonoBehaviour
    {
        protected Button button = default;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        /// <summary>
        /// Button click handler
        /// </summary>
        protected abstract void OnButtonClick();

        private void OnDestroy()
            => button.onClick.RemoveListener(OnButtonClick);
    }
}