using Features.Conveyor;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LineBackgorundHeightController : MonoBehaviour
    {
        [SerializeField]
        protected BaseConveyorLinesController baseConveyorLinesController;
        [SerializeField, Min(0)]
        protected float worldSizeToRectRatio = 240;
        [SerializeField, Min(0)]
        protected float offset = 0;
        protected RectTransform rectTransform = default;
        protected LayoutGroup layoutGroup = default;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            layoutGroup = GetComponentInParent<LayoutGroup>();
        }

        protected virtual void OnEnable()
        {
            SetHeight();
            baseConveyorLinesController.onLinesCountChanged += SetHeight;
        }

        protected virtual void SetHeight()
        {
            layoutGroup.enabled = false;
            rectTransform.sizeDelta = new Vector2
            (
                rectTransform.sizeDelta.x,
                (baseConveyorLinesController.ConveyorLines.Count * baseConveyorLinesController.PrefabHeight + offset) * worldSizeToRectRatio
            );
            layoutGroup.enabled = true;
        }

        protected virtual void OnDisable() => baseConveyorLinesController.onLinesCountChanged -= SetHeight;
    }
}
