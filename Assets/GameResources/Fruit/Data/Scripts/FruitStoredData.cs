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
    }
}