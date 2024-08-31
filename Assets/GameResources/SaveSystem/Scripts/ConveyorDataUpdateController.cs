namespace Features.SaveSystem
{
    using Features.Conveyor;
    using System;
    using UnityEngine;

    /// <summary>
    /// Conveyor data file update controller
    /// </summary>
    public class ConveyorDataUpdateController : MonoBehaviour
    {
        [SerializeField]
        protected ConveyorSaveData defaultData = new ConveyorSaveData();
        [Space, SerializeField]
        protected ConveyorController conveyorController = default;

        protected BaseConveyorLinesController linesController = default;
        protected ConveyorLineController lineController = default;
        protected LineControllerData loadedLineController = default;

        protected virtual void Awake() => InitConveyorController();

        protected virtual void OnEnable()
        {
            UpdateDataStaticAction.onUpdateRequire += UpdateConveyorData;
            conveyorController.onDataChange += OnConveyorControllerDataChanged;
            ConveyorController.AddLineAddingEndListener(OnLineAdded);
        }

        protected virtual void InitConveyorController()
        {
            if (ConveyorDataFileController.Instance.ConveyorSaveData == null)
            {
                ConveyorDataFileController.Instance.InitConveyorData(defaultData);
            }
            InitConveyor();
            conveyorController.InitLinesControllers();
        }

        protected virtual void InitConveyor()
        {
            conveyorController.InitData(ConveyorDataFileController.Instance.ConveyorSaveData.ConveyorControllerData);
            for (int i = 0; i < ConveyorDataFileController.Instance.ConveyorSaveData.LinesControllers.Count; i++)
            {
                linesController = conveyorController.ConveyorLinesControllers[i];
                for (int j = 0; j < ConveyorDataFileController.Instance.ConveyorSaveData.LinesControllers[i].LineControllers.Count; j++)
                {
                    if (j >= linesController.ConveyorLines.Count)
                    {
                        conveyorController.AddLinesImmediate(i);
                    }
                    lineController = linesController.ConveyorLines[j];
                    loadedLineController = ConveyorDataFileController.Instance.ConveyorSaveData.LinesControllers[i].LineControllers[j];
                    for (int l = 0; l < loadedLineController.Spawners.Count; l++)
                    {
                        if (l >= lineController.Spawners.Count)
                        {
                            lineController.AddSpawner(ConveyorDataFileController.Instance.ConveyorSaveData.LinesControllers[i].LineControllers[j].Spawners[l].Index);
                        }
                        lineController.Spawners[l].InitData(loadedLineController.Spawners[l]);
                    }
                }
            }
        }

        protected virtual void UpdateConveyorData() => ConveyorDataFileController.Instance.UpdateConveyorData(conveyorController);

        protected virtual void OnConveyorControllerDataChanged() => ConveyorDataFileController.Instance.UpdateConveyorControllerData(conveyorController.Data);

        protected virtual void OnLineAdded() => ConveyorDataFileController.Instance.UpdateLineControllerData(null, conveyorController.AddingLineIndex,
            conveyorController.ConveyorLinesControllers[conveyorController.AddingLineIndex].ConveyorLines.Count - 1);

        protected virtual void OnDisable()
        {
            UpdateDataStaticAction.onUpdateRequire -= UpdateConveyorData;
            conveyorController.onDataChange -= OnConveyorControllerDataChanged;
            ConveyorController.RemoveLineAddingEndListener(OnLineAdded);
        }
    }
}