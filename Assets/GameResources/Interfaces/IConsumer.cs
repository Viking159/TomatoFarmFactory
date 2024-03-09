namespace Features.Interfaces
{
    using Features.Data.BaseContainerData;

    /// <summary>
    /// Consumer object
    /// </summary>
    public interface IConsumer
    {
        /// <summary>
        /// Name of consumable object
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Consume consumable objects
        /// </summary>
        /// <param name="consumable">consumable object</param>
        public void Consume(IConsumable consumable);
    }
}