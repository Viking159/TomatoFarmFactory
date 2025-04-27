namespace Features.InfoBox
{
    using UnityEngine;
    using UnityEngine.UI;

    public class IncomeInfoBox : InfoBox
    {
        [SerializeField]
        protected Text incomeText = default;

        public virtual void Init(string text) => incomeText.text = text;
    }
}