namespace Features.InfoBox
{
    using Features.Spawner;
    using System;
    using SpawnerData = SaveSystem.SpawnerData;

    /// <summary>
    /// Abstract object creator info box
    /// </summary>
    public class BaseCreatorInfoBox : InfoBox
    {
        /// <summary>
        /// Change data event
        /// </summary>
        public event Action onDataChange = delegate { };

        /// <summary>
        /// Spawner data
        /// </summary>
        public virtual SpawnerData SpawnerData { get; protected set; } = new SpawnerData();

        /// <summary>
        /// Creator
        /// </summary>
        public AbstractObjectCreator Creator => creator;
        protected AbstractObjectCreator creator = default;

        public virtual void InitData(AbstractObjectCreator creator, SpawnerData spawnerData)
        {
            SpawnerData = spawnerData;
            this.creator = creator;
            NotifyOnDataChange();
            this.creator.onDataChange += NotifyOnDataChange;
        }

        protected virtual void NotifyOnDataChange() => onDataChange();

        protected virtual void OnDisable()
        {
            if (creator != null)
            {
                creator.onDataChange -= NotifyOnDataChange;
            }
        }
    }
}