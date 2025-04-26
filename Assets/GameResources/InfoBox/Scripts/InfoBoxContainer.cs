namespace Features.InfoBox
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(InfoBoxContainer), menuName = "Features/InfoBox/" + nameof(InfoBoxContainer))]
    public class InfoBoxContainer : ScriptableObject
    {
        public InfoBox InfoBoxPrefab => infoBoxPrefab;
        [SerializeField]
        protected InfoBox infoBoxPrefab;

        public InfoBox InfoBox => infoBox;
        protected InfoBox infoBox = default;

        public virtual void Init(InfoBox infoBoxPrefab) => infoBox = infoBoxPrefab;
    }
}
