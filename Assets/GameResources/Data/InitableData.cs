namespace Features.Data
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Initable data container
    /// </summary>
    public abstract class InitableData : ScriptableObject, IDisposable
    {
        /// <summary>
        /// Init data
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Dispose data
        /// </summary>
        public abstract void Dispose();
    }
}