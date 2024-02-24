namespace Features.Data.BaseContainerData
{
    using System;

    /// <summary>
    /// Abstract notify data container
    /// </summary>
    public abstract class AbstractNotifyData<T> : AbstractDataContainer<T>
    {
        /// <summary>
        /// Value change event
        /// </summary>
        public event Action onValueChange = delegate { }; 

        /// <summary>
        /// data value
        /// </summary>
        public virtual T DataValue
        {
            get => dataValue;
            set
            {
                if (!value.Equals(dataValue))
                {
                    dataValue = value;
                    onValueChange();
                }
            }
        }

        protected virtual void Notify()
            => onValueChange();
    }
}