namespace Features.InfoBox
{
    using UnityEngine;

    public abstract class AbstractInfoBoxController : AbstractClickController
    {
        [SerializeField]
        protected InfoBoxContainer infoBoxContainer = default;

        protected override void ClickHandle() => SpawnInfoBox();

        protected abstract void SpawnInfoBox();
    }
}

