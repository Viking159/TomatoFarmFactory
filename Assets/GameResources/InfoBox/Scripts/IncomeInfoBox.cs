namespace Features.InfoBox
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class IncomeInfoBox : InfoBox
    {
        public event Action onInit = delegate { };

        public int Income { get; protected set; } = default;
        [SerializeField]
        protected Text incomeText = default;

        public virtual void Init(int income)
        {
            Income = income;
            incomeText.text = Income.ToString();
            NotifyOnInit();
        }

        protected virtual void NotifyOnInit() => onInit();
    }
}