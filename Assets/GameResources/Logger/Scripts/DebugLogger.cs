namespace Features.Logger
{
    using UnityEngine;

    [DefaultExecutionOrder(-999)]
    public sealed class DebugLogger : MonoBehaviour
    {
        private DebugLogger _instance = default;

        [SerializeField]
        private Transform _logsParent = default;
        [SerializeField]
        private Log _logPrefab = default;

        private void Awake()
        {
            if (_instance !=  null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Application.logMessageReceived += HandleLog;
        }

        private void HandleLog(string condition, string stackTrace, LogType type)
        {
            string prefix = string.Empty;
            switch (type)
            {
                case LogType.Error:
                    prefix = $"<color=\"#FF0000\">Error</color>: ";
                    break;
                case LogType.Assert:
                    prefix = $"Assert: ";
                    break;
                case LogType.Warning:
                    prefix = $"<color=\"#FFFF00\">Warning</color>: ";
                    break;
                case LogType.Exception:
                    prefix = $"<color=\"#FF0000\">Exception</color>: ";
                    break;
                case LogType.Log:
                default:
                    prefix = $"Log: ";
                    break;
            }
            Instantiate(_logPrefab, _logsParent).Init(prefix + condition);
        }

        private void OnDestroy() => Application.logMessageReceived -= HandleLog;
    }
}
