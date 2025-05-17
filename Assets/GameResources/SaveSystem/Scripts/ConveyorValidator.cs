namespace Features.SaveSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UnityEngine;

    public class ConveyorValidator
    {
        private readonly string path;
        private readonly int maxFabricLinesCount;
        private readonly int maxFermLinesCount;
        private readonly int maxFabricsPerLine;
        private readonly int maxFermsPerLine;

        public ConveyorValidator(string path)
            : this(path, 2, 5, 3, 2) { }

        public ConveyorValidator(string path, int maxFabricLinesCount, int maxFermLinesCount, int maxFabricsPerLine, int maxFermsPerLine)
        {
            this.path = path;
            this.maxFabricLinesCount = maxFabricLinesCount;
            this.maxFermLinesCount = maxFermLinesCount;
            this.maxFabricsPerLine = maxFabricsPerLine;
            this.maxFermsPerLine = maxFermsPerLine;
        }

        public string GetJsonString()
        {
            string rawJson = File.Exists(path) ? File.ReadAllText(path) : string.Empty;
            string correctedJson = ValidateAndCleanJson(rawJson);
            File.WriteAllBytes(path, System.Text.Encoding.UTF8.GetBytes(correctedJson));
            return correctedJson;
        }

        private string ValidateAndCleanJson(string rawJson)
        {
            JObject root;
            try
            {
                root = JObject.Parse(rawJson);
            }
            catch (Exception e)
            {
                Debug.LogWarning("JSON повреждён или пуст. Создаём новый объект. Ошибка: " + e.Message);
                root = new JObject();
            }

            // --- ConveyorControllerData ---
            JObject controllerData = root["ConveyorControllerData"] as JObject;
            JObject safeController = new JObject
            {
                ["Level"] = controllerData?.Value<int?>("Level") ?? 0
            };

            // --- LinesControllers ---
            JArray sourceLines = root["LinesControllers"] as JArray;
            if (sourceLines == null)
            {
                sourceLines = new JArray();
            }

            while (sourceLines.Count < 4)
            {
                sourceLines.Add(new JObject
                {
                    ["LineControllers"] = new JArray()
                });
            }

            JArray cleanedLines = new JArray();

            for (int i = 0; i < sourceLines.Count; i++)
            {
                JObject linesController = sourceLines[i] as JObject;
                if (linesController == null) continue;

                JArray sourceLineControllers = linesController["LineControllers"] as JArray;
                if (sourceLineControllers == null)
                {
                    cleanedLines.Add(new JObject
                    {
                        ["LineControllers"] = new JArray()
                    });
                    continue;
                }

                JArray cleanedLineControllers = new JArray();

                int maxLines = i == 1 ? maxFabricLinesCount :
                               i == 3 ? maxFermLinesCount : int.MaxValue;

                int maxSpawnersPerLine = i == 1 ? maxFabricsPerLine :
                                         i == 3 ? maxFermsPerLine : int.MaxValue; ;
                int currentMaxSpawnersPerLine = maxSpawnersPerLine;

                int allowedLineCount = Math.Min(sourceLineControllers.Count, maxLines);
                for (int j = 0; j < allowedLineCount; j++)
                {
                    JObject lineController = sourceLineControllers[j] as JObject;
                    if (lineController == null) continue;

                    JArray spawners = lineController["Spawners"] as JArray;
                    if (spawners == null || spawners.Count == 0)
                    {
                        cleanedLineControllers.Add(new JObject { ["Spawners"] = new JArray() });
                        continue;
                    }

                    Dictionary<int, JObject> uniqueSpawners = new Dictionary<int, JObject>(spawners.Count);
                    if (i == 3 && j == 0)
                    {
                        currentMaxSpawnersPerLine = 1;
                    }
                    else
                    {
                        currentMaxSpawnersPerLine = maxSpawnersPerLine;
                    }
                    for (int k = 0; k < spawners.Count; k++)
                    {
                        JObject spawner = spawners[k] as JObject;
                        if (spawner == null) continue;

                        JToken indexToken = spawner["Index"];
                        if (indexToken == null || !indexToken.Type.HasFlag(JTokenType.Integer)) continue;

                        int index = indexToken.Value<int>();
                        if (uniqueSpawners.ContainsKey(index)) continue;

                        JObject cleanedSpawner = new JObject
                        {
                            ["Index"] = index,
                            ["Level"] = spawner.Value<int?>("Level") ?? 0,
                            ["Rang"] = spawner.Value<int?>("Rang") ?? 0
                        };

                        uniqueSpawners.Add(index, cleanedSpawner);

                        if (uniqueSpawners.Count >= currentMaxSpawnersPerLine)
                            break;
                    }

                    JArray finalSpawners = new JArray();
                    foreach (var s in uniqueSpawners.Values)
                        finalSpawners.Add(s);

                    JObject cleanedLineController = new JObject
                    {
                        ["Spawners"] = finalSpawners
                    };
                    cleanedLineControllers.Add(cleanedLineController);

                    uniqueSpawners.Clear();
                }

                JObject cleanedLinesController = new JObject
                {
                    ["LineControllers"] = cleanedLineControllers
                };
                cleanedLines.Add(cleanedLinesController);
            }

            JObject finalRoot = new JObject
            {
                ["ConveyorControllerData"] = safeController,
                ["LinesControllers"] = cleanedLines
            };

            return JsonConvert.SerializeObject(finalRoot, Formatting.Indented);
        }
    }
}
