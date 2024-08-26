namespace Features.Data
{
    using Features.Extensions.Data.UpdateableParam;
    using UnityEngine;

    /// <summary>
    /// Abstract class for storeable SO
    /// </summary>
    public abstract class StoreableSO : ScriptableObject
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name => dataName;
        [SerializeField]
        protected string dataName = string.Empty;

        /// <summary>
        /// Max level
        /// </summary>
        public virtual int MaxLevel => maxLevel;
        [SerializeField]
        protected int maxLevel = 25;

        [SerializeField]
        protected IntUpdateableParam updateLevelPrice = new IntUpdateableParam
        {
            ParamValue = 10,
            Ratio = 1.05f
        };

        /// <summary>
        /// Update level price
        /// </summary>
        public virtual int GetUpdateLevelPrice(int level) => updateLevelPrice.GetGrowthValue(level);

        #region GameSettings
        protected virtual void Awake()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
        public IntUpdateableParam LevelPrice => updateLevelPrice;
        public virtual void SetStartLevelPrice(int startVal) => updateLevelPrice.ParamValue = startVal;
        public virtual void SetStartLevelRatio(float ratio) => updateLevelPrice.Ratio = ratio;
        #endregion
    }
}