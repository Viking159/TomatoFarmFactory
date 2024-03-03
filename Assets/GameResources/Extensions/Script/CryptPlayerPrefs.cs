namespace Extensions.Data
{
    using UnityEngine;

    /// <summary>
    /// Crypted PlayerPrefs
    /// </summary>
    public static class CryptPlayerPrefs
    {
        private static TextEncrypter _textEncrypter = new TextEncrypter();

        private const string BOOL_TRUE_VALUE = "1";
        private const string BOOL_FALSE_VALUE = "0";

        /// <summary>
        /// Set string to PlayerPrefs
        /// </summary>
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(EncryptString(key), EncryptString(value));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Set int to PlayerPrefs
        /// </summary>
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetString(EncryptString(key), EncryptString(value.ToString()));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Set float to PlayerPrefs
        /// </summary>
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetString(EncryptString(key), EncryptString(value.ToString()));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Set float to PlayerPrefs
        /// </summary>
        public static void SetBool(string key, bool value)
        {

            PlayerPrefs.SetString(EncryptString(key), EncryptString(value ? BOOL_TRUE_VALUE : BOOL_FALSE_VALUE));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Get string from PlayerPrefs
        /// </summary>
        public static string GetString(string key, string defaultValue = "")
        {
            return DecryptString(PlayerPrefs.GetString(DecryptString(key), defaultValue));
        }

        /// <summary>
        /// Get int from PlayerPrefs
        /// </summary>
        public static int GetInt(string key, int defaultValue = 0)
        {
            string decryptKey = DecryptString(key);
            if (PlayerPrefs.HasKey(decryptKey))
            {
                string strVal = DecryptString(PlayerPrefs.GetString(decryptKey));
                if (int.TryParse(strVal, out int res))
                {
                    return res;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get float from PlayerPrefs
        /// </summary>
        public static float GetFloat(string key, float defaultValue = 0)
        {
            string decryptKey = DecryptString(key);
            if (PlayerPrefs.HasKey(decryptKey))
            {
                string strVal = DecryptString(PlayerPrefs.GetString(decryptKey));
                if (float.TryParse(strVal, out float res))
                {
                    return res;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get float from PlayerPrefs
        /// </summary>
        public static bool GetBool(string key, bool defaultValue = false)
        {
            string decryptKey = DecryptString(key);
            if (PlayerPrefs.HasKey(decryptKey))
            {
                string strVal = DecryptString(PlayerPrefs.GetString(decryptKey));
                if (strVal == BOOL_FALSE_VALUE || strVal == BOOL_TRUE_VALUE)
                {
                    return strVal == BOOL_TRUE_VALUE;
                }
            }
            return defaultValue;
        }


        private static string EncryptString(string text)
            => _textEncrypter.EncryptString(text);

        private static string DecryptString(string encryptText)
            => _textEncrypter.DecryptString(encryptText);
    }
}