namespace Features.InfoBox
{
    using Features.Data;
    using Features.Extensions;
    using UnityEngine;

    /// <summary>
    /// Switch object depends on max level
    /// </summary>
    public class LevelObjectSwither : BaseObjectSwitcher
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetView();
            baseCreatorInfoBox.onDataChange += SetView;
        }

        protected virtual void SetView()
            => SetObjects(baseCreatorInfoBox.SpawnerData.Level == data.MaxLevel);

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetView;
    }
}