namespace Features.Extensions.View
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Base class for MonoBehaviour with text component
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class BaseTextView : MonoBehaviour
    {
        protected Text text = default;

        protected virtual void Awake()
            => text = GetComponent<Text>();

        protected virtual void SetView(string str)
            => text.text = str;
    }
}