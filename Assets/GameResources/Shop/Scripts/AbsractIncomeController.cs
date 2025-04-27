namespace Features.Shop
{
    using UnityEngine;

    public abstract class AbsractIncomeController : MonoBehaviour
    {
        public abstract int GetIncome(float timeDelta);
    }
}