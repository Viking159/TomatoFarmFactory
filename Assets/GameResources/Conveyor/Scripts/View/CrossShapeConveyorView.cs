namespace Features.Conveyor
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class CrossShapeConveyorView : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> openingObjects = new List<GameObject>();
        [SerializeField]
        protected List<GameObject> closingObjects = new List<GameObject>();
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

        protected virtual void SetOpenView() => SetView(true);

        protected virtual void SetCloseView() => SetView(false);

        protected virtual void SetView(bool isOpen)
        {
            openingObjects.ForEach(obj => obj.SetActive(isOpen));
            closingObjects.ForEach(obj => obj.SetActive(!isOpen));
        }

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
