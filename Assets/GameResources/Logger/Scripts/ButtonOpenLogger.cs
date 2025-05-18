namespace Features.Logger
{
    using Features.Extensions.View;
    using UnityEngine;

    public sealed class ButtonOpenLogger : AbstractButtonView
    {
        [SerializeField, Min(1)]
        private int clicksCount = 10;

        private int currentClicksCount = 0;

        private void OnEnable() => currentClicksCount = 0;

        protected override void OnButtonClick()
        {
            currentClicksCount++;
            if (currentClicksCount >= clicksCount && DebugLogger.Instance != null)
            {
                DebugLogger.Instance.ShowPasswordFiled();
                currentClicksCount = 0;
            }
        }
    }
}
