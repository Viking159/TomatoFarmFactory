namespace Features.InfoBox
{
    using Features.Extensions.View;
    using UnityEngine;

    /// <summary>
    /// Name info text
    /// </summary>
    public class InfoNameText : MaskedTextView
    {
        [SerializeField]
        protected BaseCreatorInfoBox infoBoxController = default;
        //TODO: Get elements in line from LineController
        [SerializeField, Min(1)]
        protected int lineElementsCount = 3;

        protected virtual void OnEnable()
        {
            SetText();
            infoBoxController.onDataChange += SetText;
        }

        protected virtual void SetText()
            => SetView(GetIndex());

        protected virtual int GetIndex()
        {
            if (IsDataInited())
            {
                return infoBoxController.Creator.ConveyorLineController.Index * lineElementsCount + infoBoxController.SpawnerData.Index + 1;
            }
            return 1;
        }

        protected virtual bool IsDataInited() => infoBoxController != null 
                && infoBoxController.SpawnerData != null
                && infoBoxController.Creator != null
                && infoBoxController.Creator.ConveyorLineController != null;

        protected virtual void OnDisable()
            => infoBoxController.onDataChange -= SetText;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (infoBoxController == null)
            {
                infoBoxController = GetComponentInParent<BaseCreatorInfoBox>();
            }
        }
#endif
    }
}