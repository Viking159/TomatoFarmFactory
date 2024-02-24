namespace Features.Data.BaseContainerData
{
    using UnityEngine;

    /// <summary>
    /// Abstract data container
    /// </summary>
    public abstract class AbstractDataContainer<T> : ScriptableObject
    {
        [SerializeField]
        protected T dataValue = default;
    }
}