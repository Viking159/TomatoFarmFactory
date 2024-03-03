namespace Features.Data
{
    using System;
    using UnityEngine;
    using Extensions.Data;

    /// <summary>
    /// Abstract class for stored data
    /// </summary>
    public abstract class AbstractStoredData<T> where T : class
    {
        protected readonly string ppKey = "defaultPPKey";

        public AbstractStoredData(string key)
            => ppKey = key;

        /// <summary>
        /// Load data from player prefs
        /// </summary>
        public virtual T LoadData()
        {
            string json = CryptPlayerPrefs.GetString(ppKey);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonUtility.FromJson<T>(json);
        }

        /// <summary>
        /// Save data to player prefs
        /// </summary>
        public virtual void SaveData(T dataObject)
        {
            string json = JsonUtility.ToJson(dataObject);
            CryptPlayerPrefs.SetString(ppKey, json);
        }
    }
}