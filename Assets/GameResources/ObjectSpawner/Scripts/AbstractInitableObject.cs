namespace Features.Spawner
{
    using System;

    /// <summary>
    /// Abtsract object to init
    /// </summary>
    public abstract class AbstractInitableObject<T> : AbstractSpawnObject
    {
        /// <summary>
        /// Data init event
        /// </summary>
        public event Action onDataInited = delegate { };

        public T Data => data;
        protected T data = default;

        public virtual void InitData(T data, int level)
        {
            this.data = data;
            this.level = level;
            NotifyDataInit();
        }

        protected virtual void NotifyDataInit()
            => onDataInited();
    }
}