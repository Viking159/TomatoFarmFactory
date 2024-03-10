namespace Features.Data
{
    using Extensions.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Abstract class for storeable SO
    /// </summary>
    public abstract class StoreableSO : ScriptableObject
    {
        /// <summary>
        /// Data change event
        /// </summary>
        public event Action onDataChange = delegate { };

        [SerializeField]
        protected string ppKey = "defaultKey";

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name => dataName;
        [SerializeField]
        protected string dataName = string.Empty;

        /// <summary>
        /// Level
        /// </summary>
        public virtual int Level => level;
        [SerializeField]
        protected int level = default;

        /// <summary>
        /// Load data
        /// </summary>
        public virtual void LoadData()
            => level = CryptPlayerPrefs.GetInt(ppKey, level);

        /// <summary>
        /// Save data
        /// </summary>
        public virtual void SaveData()
            => CryptPlayerPrefs.SetInt(ppKey, level);

        /// <summary>
        /// Set level
        /// </summary>
        public virtual void SetLevel(int newValue)
        {
            level = Mathf.Max(0, newValue);
            SaveData();
            Notify();
        }

        protected virtual void Notify()
            => onDataChange();
    }
}