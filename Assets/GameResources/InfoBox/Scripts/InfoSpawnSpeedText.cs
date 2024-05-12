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
        protected SpawnerData data = default;
        [SerializeField]
        protected int digitsCount = default;

        protected virtual void OnEnable()
        {
            SetText();
            data.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(Math.Round(GlobalData.SPEED_CONVERT_RATIO/data.Speed, digitsCount));

        protected virtual void OnDisable()
            => data.onDataChange -= SetText;
    }
}