namespace Features.Fruit.Data
{
    using System;

    /// <summary>
    /// Fruit data to store
    /// </summary>
    [Serializable]
    public class FruitStoredData
    {
        public string Name = string.Empty;
        public float Price = 0;
        public int Level = 0;

        public FruitStoredData Clone()
            => new FruitStoredData()
            {
                Name = Name,
                Price = Price,
                Level = Level
            };
    }
}