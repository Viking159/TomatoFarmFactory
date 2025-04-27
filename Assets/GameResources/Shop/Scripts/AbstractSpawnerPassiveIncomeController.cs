namespace Features.Shop
{
    using Features.Conveyor;
    using UnityEngine;

    public abstract class AbstractSpawnerPassiveIncomeController : AbsractIncomeController
    {
        [SerializeField]
        protected int saleTime = 90;
        [SerializeField]
        protected ConveyorController conveyorController = default;
        [SerializeField]
        protected int lineIndex = 1;

        protected int salesCount = default;

        protected abstract int GetCount(int level);

        protected abstract int GetPrice(int level);

        public override int GetIncome(float deltaSeconds)
        {
            int income = 0;
            if (deltaSeconds > saleTime)
            {
                ConveyorLineController conveyorLine = default;
                salesCount = (int)(deltaSeconds / saleTime);
                for (int i = 0; i < conveyorController.ConveyorLinesControllers[lineIndex].ConveyorLines.Count; i++)
                {
                    conveyorLine = conveyorController.ConveyorLinesControllers[lineIndex].ConveyorLines[i];
                    for (int j = 0; j < conveyorLine.Spawners.Count; j++)
                    {
                        income += GetCount(conveyorLine.Spawners[j].Rang) * GetPrice(0);
                    }
                }
            }
            income *= salesCount;
            return income;
        }
    }
}