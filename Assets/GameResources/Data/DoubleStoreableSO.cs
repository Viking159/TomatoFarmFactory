namespace Features.Data
{
    using Features.Extensions.Data;
    using Features.Extensions.Data.UpdateableParam;
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public abstract class DoubleStoreableSO : StoreableSO
    {
        /// <summary>
        /// Rang
        /// </summary>
        public int Rang => rang;
        [SerializeField]
        protected int rang = default;

        /// <summary>
        /// Max rang
        /// </summary>
        public int MaxRang => maxRang;
        [SerializeField]
        private int maxRang = 5;

        /// <summary>
        /// Update rang price
        /// </summary>
        public int UpdateRangPrice => updateLevelPrice.GetGrowthValue(level);
        [SerializeField]
        protected IntUpdateableParam updateRangPrice = new IntUpdateableParam
        {
            ParamValue = 0,
            GrowthPercent = 0
        };

        public override void LoadData()
        {
            base.LoadData();
            rang = CryptPlayerPrefs.GetInt(ppKey + nameof(rang), rang);
        }

        public override void SaveData()
        {
            base.SaveData();
            CryptPlayerPrefs.SetInt(ppKey + nameof(rang), rang);
        }

        /// <summary>
        /// Set rang value
        /// </summary>
        public virtual void SetRang(int newValue)
        {
            int clampedValue = Mathf.Clamp(newValue, MIN_LEVEL, maxRang);
            if (rang != clampedValue)
            {
                level = 0;
                rang = clampedValue;
                SaveData();
                Notify();
            }
        }
    }
}