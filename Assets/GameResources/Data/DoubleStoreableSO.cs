namespace Features.Data
{
    using Features.Extensions.Data;
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
        /// <param name="newValue"></param>
        public virtual void SetRang(int newValue)
        {
            rang = Mathf.Max(0, newValue);
            SaveData();
            Notify();
        }
    }
}