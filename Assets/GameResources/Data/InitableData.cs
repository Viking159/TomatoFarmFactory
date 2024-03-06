namespace Features.Data
{
    using UnityEngine;

    /// <summary>
    /// Initable data container
    /// </summary>
    public abstract class InitableData : ScriptableObject
    {
        /// <summary>
        /// Init
        /// </summary>
        public abstract void Init();
    }
}