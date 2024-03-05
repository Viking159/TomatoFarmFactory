namespace Features.Temp.Prekol
{
    using Features.Conveyor.Data;
    using Features.Data;
    using Features.Fruit.Data;
    using System.Collections.Generic;
    using UnityEngine;

    public class Temp : MonoBehaviour
    {
        [SerializeField]
        private ConveyorData conveyorData = default;
        [SerializeField]
        private FruitData fruitData = default;

        public void Print()
        {
            Debug.Log($"{fruitData.Name}: {fruitData.Price}; {fruitData.Level}; {fruitData.Count}");
            Debug.Log($"conveyorData: {conveyorData.Speed}; {conveyorData.Level}");
        }

    }
}