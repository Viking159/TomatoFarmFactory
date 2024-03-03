namespace Features.Data
{
    using UnityEngine;
    using Extensions.Data;

    /// <summary>
    /// Abstract class for controller with serializable data
    /// </summary>
    public abstract class AbstractStoredDataController<T> : MonoBehaviour where T : class
    {
        protected abstract void InitData();

        protected virtual string GetData(T data)
            => JsonUtility.ToJson(data);

        protected virtual T LoadData(string key)
        {
            string json = CryptPlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonUtility.FromJson<T>(json);
        }

        protected virtual void SaveData(string key, T data)
        {
            string json = GetData(data);
            CryptPlayerPrefs.SetString(key, json);
        }
    }
}