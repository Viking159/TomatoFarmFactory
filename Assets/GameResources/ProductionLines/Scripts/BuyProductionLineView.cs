namespace Features.BuyProductionLineView
{
    using Features.Conveyor;
    using Features.Extensions;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Buy new production line view
    /// </summary>
    public class BuyProductionLineView : BaseObjectSwitcher
    {
        [SerializeField]
        protected ConveyorController conveyorController = default;
        [SerializeField, Min(0)]
        protected int lineIndex = 0;
        [SerializeField, Min(0)]
        protected int maxLinesCount = 5;

        protected ConveyorLineController currentLineController = default;

        protected virtual void OnEnable()
        {
            OnLineAddEnded();
            ConveyorController.AddLineAddingStartListener(OnLineAddStarted);
            ConveyorController.AddLineAddingEndListener(OnLineAddEnded);
        }

        protected virtual void OnLineAddStarted()
        {
            StopListenLineController();
            DisableView();
        }

        protected virtual void OnLineAddEnded()
        {
            currentLineController = conveyorController.ConveyorLinesControllers[lineIndex].ConveyorLines.LastOrDefault();
            SetView();
            ListenLineController();
        }


        protected virtual void ListenLineController()
        {
            if (currentLineController != null)
            {
                currentLineController.onSpawnerAdd += SetView;
            }
        }

        protected virtual void DisableView() => SetObjects(false);

        protected virtual void SetView() => SetObjects(IsValidView());

        protected virtual bool IsValidView() => LastLineFinished() && !IsMaxLineCount();

        protected virtual bool IsMaxLineCount() => conveyorController.ConveyorLinesControllers[lineIndex].ConveyorLines.Count >= maxLinesCount;

        protected virtual bool LastLineFinished() => true || currentLineController == null
                || currentLineController.Spawners.Count == currentLineController.MaxSpawnersCount;

        protected virtual void StopListenLineController()
        {
            if (currentLineController != null)
            {
                currentLineController.onSpawnerAdd -= SetView;
            }
        }

        protected virtual void OnDisable()
        {
            ConveyorController.RemoveLineAddingStartListener(OnLineAddStarted);
            ConveyorController.RemoveLineAddingEndListener(SetView);
            StopListenLineController();
        }
    }
}