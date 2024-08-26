namespace Features.AbstractFloatStart
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(InputField))]
    public abstract class AbstractFloatSet<T> : MonoBehaviour
    {
        [SerializeField]
        protected T data = default;
        protected InputField inputField = default;

        protected virtual void Awake() => inputField = GetComponent<InputField>();

        protected abstract float paramValue { get; }

        protected abstract void SetData(float res);

        protected virtual void OnEnable()
        {
            inputField.placeholder.GetComponent<Text>().text = paramValue.ToString();
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        protected virtual void OnEndEdit(string val)
        {
            val = val.Replace('.', ',');
            if (float.TryParse(val, out float res))
            {
                SetData(res);
            }
            else
            {
                inputField.text = paramValue.ToString();
            }
        }

        protected virtual void OnDisable()
            => inputField.onEndEdit.RemoveListener(OnEndEdit);
    }
}