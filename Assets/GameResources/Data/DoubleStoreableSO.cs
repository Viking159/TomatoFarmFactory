namespace Features.Data
{
    using Features.Extensions.Data;
    using Features.Extensions.Data.UpdateableParam;
    using UnityEngine;

    /// <summary>
    /// Abstract class for storeable SO with two storeable datas
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
        public int UpdateRangPrice => updateLevelPrice.GetGrowthValue(rang);
        [SerializeField]
        protected IntUpdateableParam updateRangPrice = new IntUpdateableParam
        {
            ParamValue = 1,
            Ratio = 0.1f
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