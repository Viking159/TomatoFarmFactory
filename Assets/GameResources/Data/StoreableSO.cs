namespace Features.Data
{
    using Extensions.Data;
    using Features.Extensions.Data.UpdateableParam;
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
        /// Max level
        /// </summary>
        public virtual int MaxLevel => maxLevel;
        [SerializeField]
        protected int maxLevel = 25;

        /// <summary>
        /// Update level price
        /// </summary>
        public int UpdateLevelPrice => updateLevelPrice.GetGrowthValue(level);
        [SerializeField]
        protected IntUpdateableParam updateLevelPrice = new IntUpdateableParam
        {
            ParamValue = 10,
            Ratio = 0.05f
        };

        [SerializeField]
        protected string ppKey = "defaultKey";

        protected const int MIN_LEVEL = 0;

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
            int clampedValue = Mathf.Clamp(newValue, MIN_LEVEL, maxLevel);
            if (level != clampedValue)
            {
                level = clampedValue;
                SaveData();
                Notify();
            }
        }

        protected virtual void Notify()
            => onDataChange();
    }
}