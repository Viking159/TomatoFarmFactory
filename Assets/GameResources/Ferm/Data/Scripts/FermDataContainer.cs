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
        /// Speed
        /// </summary>
        public float Speed => fermData.Speed;

        /// <summary>
        /// Level
        /// </summary>
        public int Level => fermData.Level;

        /// <summary>
        /// Rang
        /// </summary>
        public int Rang => fermData.Rang;

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