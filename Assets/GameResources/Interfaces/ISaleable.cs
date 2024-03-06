namespace Features.Interfaces
{
    /// <summary>
    /// Objects for sale interface
    /// </summary>
    public interface ISaleable
    {
        /// <summary>
        /// Object name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Object price
        /// </summary>
        public int Price { get; }

        /// <summary>
        /// Object count
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Sale object
        /// </summary>
        public void Sale();
    }
}