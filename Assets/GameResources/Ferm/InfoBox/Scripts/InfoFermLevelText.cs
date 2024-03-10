namespace Features.Ferm.InfoBox
{
    using Features.Extensions.View;
    using Features.Ferm.Data;
    using UnityEngine;

    /// <summary>
    /// Ferm name info text
    /// </summary>
    public class InfoFermLevelText : BaseTextView
    {
        [SerializeField]
        protected FermDataContainer fermDataContainer = default;
        [SerializeField]
        protected string mask = "Level: {0}";

        protected virtual void OnEnable()
        {
            SetText();
            fermDataContainer.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(string.Format(mask, fermDataContainer.Level));

        protected virtual void OnDisable()
            => fermDataContainer.onDataChange -= SetText;
    }
}