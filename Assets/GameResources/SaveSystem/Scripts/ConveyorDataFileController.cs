namespace Features.SaveSystem
{
    using Features.Conveyor;
    using Features.Data.BaseContainerData;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// Conveyor file controller
    /// </summary>
    public class ConveyorDataFileController : AbstractDataFileController
    {
        protected const int SAVE_AWAIT_TIME = 2;

        /// <summary>
        /// Conveyor save data
        /// </summary>
        public ConveyorSaveData ConveyorSaveData => conveyorSaveData;
        protected ConveyorSaveData conveyorSaveData = default;

        /// <summary>
        /// ConveyorDataFileController insatnce
        /// </summary>
        public static ConveyorDataFileController Instance => instance;
        protected static ConveyorDataFileController instance = default;

        [SerializeField]
        protected string dataFolder = "ConveyorData";
        [SerializeField]
        protected StringNotifyData nextGameNameContainer = default;

        protected BaseConveyorLinesController linesController = default;
        protected ConveyorLineController lineController = default;

        protected string dataJson = string.Empty;
        protected string folderPath = default;
        protected string path = default;

        protected bool queuedSave = false;
        protected float lastSaveTime = -1;

        /// <summary>
        /// Load game data
        /// </summary>
        public override async void LoadData()
        {
            IsInited = false;
            folderPath = Path.Combine(Application.persistentDataPath, dataFolder);
            path = Path.Combine(folderPath, string.Format(JSON_FILE, nextGameNameContainer.DataValue));
            if (File.Exists(path))
            {
                try
                {
                    dataJson = await File.ReadAllTextAsync(path);
                    conveyorSaveData = JsonUtility.FromJson<ConveyorSaveData>(dataJson);
                    IsInited = true;
                    NotifyOnDataInit();
                    return;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{nameof(ConveyorDataFileController)}: Load data error: {ex.Message}\n{ex.StackTrace}");
                }
            }
            conveyorSaveData = default;
            IsInited = true;
            NotifyOnDataInit();
        }

        /// <summary>
        /// Save conveyor data
        /// </summary>
        public virtual async void SaveData()
        {
            try
            {
                if (Time.time - lastSaveTime < SAVE_AWAIT_TIME)
                {
                    if (!queuedSave)
                    {
                        queuedSave = true;
                        await Task.Delay(TimeSpan.FromSeconds(SAVE_AWAIT_TIME));
                        SaveData();
                    }
                    return;
                }
                queuedSave = false;
                lastSaveTime = Time.time;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                dataJson = JsonUtility.ToJson(conveyorSaveData, true);
                await File.WriteAllTextAsync(path, dataJson);
                Debug.Log($"{nameof(ConveyorDataFileController)}: Data saved");
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(ConveyorDataFileController)}: Save data error: {ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Init conveyor data
        /// </summary>
        public virtual void InitConveyorData(ConveyorSaveData conveyorSaveData)
        {
            this.conveyorSaveData = new ConveyorSaveData();
            UpdateConveyorControllerData(conveyorSaveData.ConveyorControllerData, false);
            UpdateLinesControllersData(conveyorSaveData.LinesControllers, false);
            SaveData();
        }

        /// <summary>
        /// Update full conveyor data
        /// </summary>
        public virtual void UpdateConveyorData(ConveyorController conveyorController)
        {
            UpdateConveyorControllerData(conveyorController.Data, false);
            for (int i = 0; i < conveyorController.ConveyorLinesControllers.Count; i++)
            {
                linesController = conveyorController.ConveyorLinesControllers[i];
                if (i >= conveyorSaveData.LinesControllers.Count)
                {
                    conveyorSaveData.LinesControllers.Add(new LinesControllerData());
                }
                for (int j = 0; j < linesController.ConveyorLines.Count; j++)
                {
                    lineController = linesController.ConveyorLines[j];
                    if (j >= conveyorSaveData.LinesControllers[i].LineControllers.Count)
                    {
                        conveyorSaveData.LinesControllers[i].LineControllers.Add(new LineControllerData());
                    }
                    for (int l = 0; l < lineController.Spawners.Count; l++)
                    {
                        UpdateSpawnerData(lineController.Spawners[l].SpawnerData, i, j, l, false);
                    }
                }
            }
            SaveData();
        }

        /// <summary>
        /// Update conveyor controller data
        /// </summary>
        public virtual void UpdateConveyorControllerData(ConveyorControllerData data, bool saveAfter = true)
            => UpdateData(() =>
            {
                conveyorSaveData.ConveyorControllerData = data;
            }, saveAfter);

        /// <summary>
        /// Update lines controllers data
        /// </summary>
        public virtual void UpdateLinesControllersData(List<LinesControllerData> linesControllersData, bool saveAfter = true)
            => UpdateData(() =>
            {
                for (int i = 0; i < linesControllersData.Count; i++)
                {
                    UpdateLinesControllerData(linesControllersData[i], i, false);
                }
            }, saveAfter);

        /// <summary>
        /// Update lines controller data
        /// </summary>
        public virtual void UpdateLinesControllerData(LinesControllerData linesControllerData, int linesControllerIndex, bool saveAfter = true)
            => UpdateData(() =>
            {
                if (linesControllerIndex >= conveyorSaveData.LinesControllers.Count)
                {
                    conveyorSaveData.LinesControllers.Add(new LinesControllerData());
                }
                if (linesControllerData != null)
                {
                    for (int i = 0; i < linesControllerData.LineControllers.Count; i++)
                    {
                        UpdateLineControllerData(linesControllerData.LineControllers[i], linesControllerIndex, i, false);
                    }
                }
            }, saveAfter);

        /// <summary>
        /// Update line controller data
        /// </summary>
        public virtual void UpdateLineControllerData(LineControllerData lineControllerData, int linesControllerIndex, int lineControllerIndex, bool saveAfter = true)
            => UpdateData(() =>
            {
                if (lineControllerIndex >= conveyorSaveData.LinesControllers[linesControllerIndex].LineControllers.Count)
                {
                    conveyorSaveData.LinesControllers[linesControllerIndex].LineControllers.Add(new LineControllerData());
                }
                if (lineControllerData != null)
                {
                    for (int i = 0; i < lineControllerData.Spawners.Count; i++)
                    {
                        UpdateSpawnerData(lineControllerData.Spawners[i], linesControllerIndex, lineControllerIndex, i, false);
                    }
                }
            }, saveAfter);

        /// <summary>
        /// Update spawner data
        /// </summary>
        public virtual void UpdateSpawnerData(SpawnerData data, int linesControllerIndex, int lineControllerIndex, int spawnerIndex, bool saveAfter = true)
            => UpdateData(() => 
            {
                if (spawnerIndex >= conveyorSaveData.LinesControllers[linesControllerIndex].LineControllers[lineControllerIndex].Spawners.Count)
                {
                    conveyorSaveData.LinesControllers[linesControllerIndex].LineControllers[lineControllerIndex].Spawners.Add(new SpawnerData());
                }
                conveyorSaveData.LinesControllers[linesControllerIndex].LineControllers[lineControllerIndex].Spawners[spawnerIndex] = data;
            }, saveAfter);

        protected virtual void UpdateData(Action update, bool saveAfter)
        {
            update();
            if (saveAfter)
            {
                SaveData();
            }
        }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
    }

    /// <summary>
    /// Game data
    /// </summary>
    [Serializable]
    public class ConveyorSaveData
    {
        /// <summary>
        /// Conveyor controller data
        /// </summary>
        public ConveyorControllerData ConveyorControllerData = new ConveyorControllerData();

        /// <summary>
        /// Lines controllers data
        /// </summary>
        public List<LinesControllerData> LinesControllers = new List<LinesControllerData>();
    }

    /// <summary>
    /// Conveyor controller data
    /// </summary>
    [Serializable]
    public class ConveyorControllerData
    {
        /// <summary>
        /// Level
        /// </summary>
        public int Level = 0;
    }

    /// <summary>
    /// Conveyor parts data
    /// </summary>
    [Serializable]
    public class LinesControllerData
    {
        /// <summary>
        /// Line controllers data
        /// </summary>
        public List<LineControllerData> LineControllers = new List<LineControllerData>();
    }

    /// <summary>
    /// Conveyor parts data
    /// </summary>
    [Serializable]
    public class LineControllerData
    {
        /// <summary>
        /// Spawners data
        /// </summary>
        public List<SpawnerData> Spawners = new List<SpawnerData>();
    }

    /// <summary>
    /// Conveyor parts data
    /// </summary>
    [Serializable]
    public class SpawnerData
    {
        /// <summary>
        /// Spawner index
        /// </summary>
        public int Index = 0;

        /// <summary>
        /// Level
        /// </summary>
        public int Level = 0;

        /// <summary>
        /// Rang
        /// </summary>
        public int Rang = 0;
    }
}