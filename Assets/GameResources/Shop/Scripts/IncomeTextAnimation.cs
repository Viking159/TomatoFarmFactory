namespace Features.IncomeTextAnimation
{
    using Features.Extensions.View;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// IncomeTextAnimation
    /// </summary>
    public class IncomeTextAnimation : BaseTextView
    {
        [SerializeField]
        protected float animationDuration = 1f;
        [SerializeField]
        protected float awaitTime = 0.01f;
        protected int income = default;
        protected int incomeTextValue = default;
        protected float time = default;
        protected Coroutine coroutine = default;

        public virtual void AnimateText(int income)
        {
            this.income = income;
            coroutine = StartCoroutine(PlayAnimation());
        }

        protected virtual IEnumerator PlayAnimation()
        {
            incomeTextValue = income;
            int finalvalue = income * 2;
            time = default;
            while (isActiveAndEnabled && time < animationDuration)
            {
                incomeTextValue = (int)Mathf.Lerp(income, finalvalue, time / animationDuration);
                text.text = incomeTextValue.ToString();
                yield return new WaitForSeconds(awaitTime);
                time += awaitTime;
            }
            text.text = finalvalue.ToString();
        }

        protected virtual void OnDisable()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}