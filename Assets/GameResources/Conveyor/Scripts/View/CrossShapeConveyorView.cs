namespace Features.Conveyor
{
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class CrossShapeConveyorView : MonoBehaviour
    {
        [SerializeField]
        protected Sprite crossOpenSprite = default;
        [SerializeField]
        protected Sprite crossCloseSprite = default;
        [SerializeField]
        protected ConveyorLineController lineController = default;

        protected BaseConveyorLinesController conveyorLinesController = default;
        protected SpriteRenderer spriteRenderer = default;

        protected virtual void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        protected virtual void OnEnable()
        {
            Subscribe();
            lineController.onInit += Subscribe;
        }

        protected virtual void Subscribe()
        {
            Unsubscribe();
            conveyorLinesController = lineController.LinesController;
            SetView();
            if (conveyorLinesController != null)
            {
                conveyorLinesController.onLinesCountChanged += SetView;
            }
        }

        protected virtual void SetView()
        {
            if (conveyorLinesController != null && lineController.Index < conveyorLinesController.ConveyorLines.Count - 1)
            {
                SetOpenView();
            }
            else
            {
                SetCloseView();
            }
        }

        protected virtual void SetOpenView() => spriteRenderer.sprite = crossOpenSprite;

        protected virtual void SetCloseView() => spriteRenderer.sprite = crossCloseSprite;

        protected virtual void Unsubscribe()
        {
            if (conveyorLinesController != null)
            {
                conveyorLinesController.onLinesCountChanged -= SetView;
            }
        }

        protected virtual void OnDisable()
        {
            Unsubscribe();
            lineController.onInit -= Subscribe;
        }
    }
}
