namespace Features.Extensions.Data
{
    using System;

    /// <summary>
    /// Generic value extension
    /// </summary>
    public class Value<T>
    {
        /// <summary>
        /// Value change event
        /// </summary>
        public event Action onValueChange = delegate { };

        /// <summary>
        /// Current value
        /// </summary>
        public virtual T CurVal => curVal;

        /// <summary>
        /// Previous value
        /// </summary>
        public virtual T PrevVal => prevVal;

        protected T curVal = default;
        protected T prevVal = default;

        /// <summary>
        /// Set value without invoke action
        /// </summary>
        public virtual void SetValueWithoutNotify(T val)
        {
            prevVal = CurVal;
            curVal = val;
        }

        /// <summary>
        /// Set value
        /// </summary>
        public virtual void SetValue(T val)
        {
            SetValueWithoutNotify(val);
            Notify();
        }

        protected virtual void Notify()
            => onValueChange();
    }
}