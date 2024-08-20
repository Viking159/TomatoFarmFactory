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
        protected StoreableSO data = default;

        protected virtual void OnEnable()
        {
            SetView();
            data.onDataChange += SetView;
        }

        protected virtual void SetView()
            => SetObjects(data.Level == data.MaxLevel);

        protected virtual void OnDisable()
            => data.onDataChange -= SetView;
    }
}