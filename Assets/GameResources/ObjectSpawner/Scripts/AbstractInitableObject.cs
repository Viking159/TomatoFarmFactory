namespace Features.Spawner
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Abtsract object to init
    /// </summary>
    public abstract class AbstractInitableObject<T> : MonoBehaviour
    {
        /// <summary>
        /// Data init event
        /// </summary>
        public event Action onDataInited = delegate { };

        public T Data => data;
        protected T data = default;

        public virtual void Init(T data)
        {
            this.data = data;
            NotifyDataInit();
        }

        protected virtual void NotifyDataInit()
            => onDataInited();
    }
}