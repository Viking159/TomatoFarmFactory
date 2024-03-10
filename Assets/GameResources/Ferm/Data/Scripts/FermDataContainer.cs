namespace Features.Ferm.Data
{
    using Features.Data;
    using System;
    using UnityEngine;

    /// <summary>
    /// Ferm data container
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FermDataContainer), menuName = "Features/Data/Ferm/" + nameof(FermDataContainer))]
    public class FermDataContainer : InitableData
    {
        /// <summary>
        /// Data change event
        /// </summary>
        public event Action onDataChange = delegate { };

        /// <summary>
        /// Name
        /// </summary>
        public string Name => fermData.Name;

        /// <summary>
        /// Speed
        /// </summary>
        public float Speed => fermData.Speed;

        /// <summary>
        /// Level
        /// </summary>
        public int Level => fermData.Level;

        /// <summary>
        /// Max level
        /// </summary>
        public int MaxLevel => fermData.MaxLevel;

        /// <summary>
        /// Update level price
        /// </summary>
        public int UpdateLevelPrice => fermData.UpdateLevelPrice;

        /// <summary>
        /// Rang
        /// </summary>
        public int Rang => fermData.Rang;

        /// <summary>
        /// Max rang
        /// </summary>
        public int MaxRang => fermData.MaxRang;

        /// <summary>
        /// Update rang price
        /// </summary>
        public int UpdateRangPrice => fermData.UpdateRangPrice;

        [SerializeField]
        protected FermData fermData = default;

        public override void Init()
            => fermData.onDataChange += Notify;

        protected virtual void Notify()
            => onDataChange();

        public override void Dispose()
            => fermData.onDataChange -= Notify;
    }
}