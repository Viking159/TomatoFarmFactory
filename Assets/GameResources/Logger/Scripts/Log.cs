namespace Features.Logger
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Log : MonoBehaviour
    {
        [SerializeField]
        protected Text text = default;

        public virtual void Init(string logText) => text.text = logText;
    }
}
