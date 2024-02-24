namespace Features.Data.BaseContainerData
{
    /// <summary>
    /// Abstract readonly data container
    /// </summary>
    public abstract class AbstractReadonlyData<T> : AbstractDataContainer<T>
    {
        /// <summary>
        /// Data value
        /// </summary>
        public T DataValue => dataValue;
    }
}