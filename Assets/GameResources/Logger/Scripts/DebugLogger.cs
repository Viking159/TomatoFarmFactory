namespace Features.Logger
{
    using UnityEngine;
    using UnityEngine.UI;

    [DefaultExecutionOrder(-999)]
    public sealed class DebugLogger : MonoBehaviour
    {
        private const string PREFS_KEY = "ShowLogger";
        private const string PASSWORD = "aDmin159";

        public static DebugLogger Instance => _instance;
        private static DebugLogger _instance = default;

        [SerializeField]
        private Transform _logsParent = default;
        [SerializeField]
        private Log _logPrefab = default;
        [SerializeField]
        private GameObject _loggerObject = default;
        [SerializeField]
        private InputField _passwordField = default;
        [SerializeField]
        private Button _acceptPasswordButton = default;

        private bool _isOpened = false;

        public void CloseLogger()
        {
            HideInputField();
            if (_isOpened)
            {
                PlayerPrefs.SetInt(PREFS_KEY, 0);
                _isOpened = false;
                _loggerObject.SetActive(false);
                Application.logMessageReceived -= HandleLog;
            }
        }

        public void ShowPasswordFiled()
        {
            _acceptPasswordButton.onClick.AddListener(HandlePasswordAccept);
            _passwordField.gameObject.SetActive(true);
            _acceptPasswordButton.gameObject.SetActive(true);
        }

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
            InitLoggerView();
        }

#if UNITY_EDITOR
        [ContextMenu("ClearPrefsData")]
        private void ClearPrefsData()
            => PlayerPrefs.DeleteKey(PREFS_KEY);
#endif

        private void InitLoggerView()
        {
            bool isVisible = PlayerPrefs.GetInt(PREFS_KEY) == 1;
            _isOpened = !isVisible;
            if (isVisible)
            {
                OpenLogger();
            }
            else
            {
                CloseLogger();
            }
        }

        private void HandlePasswordAccept()
        {
            if (_passwordField.text == PASSWORD)
            {
                OpenLogger();
            }
        }

        private void OpenLogger()
        {
            HideInputField();
            if (!_isOpened)
            {
                _isOpened = true;
                PlayerPrefs.SetInt(PREFS_KEY, 1);
                _loggerObject.SetActive(true);
                Application.logMessageReceived += HandleLog;
            }
        }

        private void HideInputField()
        {
            _acceptPasswordButton.onClick.RemoveListener(HandlePasswordAccept);
            _passwordField.gameObject.SetActive(false);
            _acceptPasswordButton.gameObject.SetActive(false);
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

        private void OnDestroy()
        {
            _acceptPasswordButton.onClick.RemoveListener(HandlePasswordAccept);
            Application.logMessageReceived -= HandleLog;
        }
    }
}
