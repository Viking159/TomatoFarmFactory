namespace Features.InfoBox
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public abstract class AbstractClickController : MonoBehaviour, IPointerClickHandler
    {
        protected PointerEventData clickEventData = default;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            clickEventData = eventData;
            ClickHandle();
        }

        protected abstract void ClickHandle();
    }
}

