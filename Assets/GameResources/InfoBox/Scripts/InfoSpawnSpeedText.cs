namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions.View;
    using Features.Spawner;
    using System;
    using UnityEngine;

    /// <summary>
    /// Spawn speed text
    /// </summary>
    public class InfoSpawnSpeedText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected SpawnerData data = default;
        [SerializeField]
        protected int digitsCount = default;

        protected virtual void OnEnable()
        {
            SetText();
            baseCreatorInfoBox.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(Math.Round(GlobalData.SPEED_CONVERT_RATIO/data.GetSpeed(baseCreatorInfoBox.SpawnerData.Level), digitsCount));

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetText;
    }
}