namespace Features.Data
{
    using Extensions.Data;
    using UnityEngine;

    /// <summary>
    /// Abstract class for storeable SO
    /// </summary>
    public abstract class StoreableSO : ScriptableObject
    {
        [SerializeField]
        protected string ppKey = "defaultKey";

        /// <summary>
        /// Fruit level
        /// </summary>
        public virtual int Level => level;
        [SerializeField]
        protected int level = default;

        /// <summary>
        /// Load data
        /// </summary>
        public virtual void LoadData()
            => level = CryptPlayerPrefs.GetInt(ppKey);

        /// <summary>
        /// Save data
        /// </summary>
        public virtual void SaveData()
            => CryptPlayerPrefs.SetInt(ppKey, level);

        /// <summary>
        /// Set level
        /// </summary>
        public virtual void SetLevel(int newValue)
            => level = Mathf.Max(0, newValue);
    }
}