namespace Features.Fruit
{
    using Features.Conveyor;
    using Features.Fruit.Data;
    using Features.Interfaces;
    using UnityEngine;

    /// <summary>
    /// Consumable fruit
    /// </summary>
    public class ConsumableFruit : MonoBehaviour, IConsumable
    {
        public string Name => fruit.Data.Name;

        public int Count => fruitData.GetCount(fruit.Level);

        [SerializeField]
        protected BaseFruit fruit = default;
        [SerializeField]
        protected ConveyorRider conveyorRider = default;
        [SerializeField]
        protected FruitData fruitData = default;

        public void Consume(IConsumer consumer)
        {
            conveyorRider.enabled = false;
            conveyorRider.KillRider();
        }
    }
}