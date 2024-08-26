namespace Features.AbstractFloatRatio
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(InputField))]
    public abstract class AbstractIntSet<T> : MonoBehaviour
    {
        [SerializeField]
        protected T data = default;
        protected InputField inputField = default;

        protected virtual void Awake() => inputField = GetComponent<InputField>();

        protected abstract int paramValue { get; }

        protected abstract void SetData(int res);

        protected virtual void OnEnable()
        {
            inputField.placeholder.GetComponent<Text>().text = paramValue.ToString();
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        protected virtual void OnEndEdit(string val)
        {
            if (int.TryParse(val, out int res))
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