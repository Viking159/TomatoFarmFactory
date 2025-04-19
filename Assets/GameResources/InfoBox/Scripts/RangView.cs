namespace Features.InfoBox
{
    using UnityEngine;
    using Features.Data;
    using System.Collections.Generic;
    using UnityEngine.UI;

    /// <summary>
    /// Rang view controller
    /// </summary>
    public class RangView : MonoBehaviour
    {
        [SerializeField]
        protected BaseCreatorInfoBox baseCreatorInfoBox = default;
        [SerializeField]
        protected List<Image> images = new List<Image>();
        [SerializeField]
        protected Color activeColor = default;
        [SerializeField]
        protected Color inactiveColor = default;

        protected virtual void OnEnable()
        {
            SetView();
            baseCreatorInfoBox.onDataChange += SetView;
        }

        protected virtual void SetView()
        {
            if (baseCreatorInfoBox.SpawnerData != null)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    images[i].color = baseCreatorInfoBox.SpawnerData.Rang >= (i + 1) ? activeColor : inactiveColor;
                }
            }
        }

        protected virtual void OnDisable()
            => baseCreatorInfoBox.onDataChange -= SetView;
    }
}