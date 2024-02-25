namespace Features.Interfaces
{
    using Features.Data.BaseContainerData;

    /// <summary>
    /// Consumable objects inteface
    /// </summary>
    public interface IConsumable
    {
        /// <summary>
        /// Name of consumable object
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Objects count
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Consume object
        /// </summary>
        /// <param name="consumer">Consumer</param>
        public void Consume(IConsumer consumer);
    }
}