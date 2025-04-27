namespace Features.Shop
{
    using System;
    using System.Collections;
    using UnityEngine;

    public sealed class LastTimeActivityController : MonoBehaviour
    {
        [SerializeField]
        private float saveInterval = 5f;

        private Coroutine saveTimeCoroutine;
        private const string TIME_KEY = "LastSavedTime";
        private const string TIME_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";

        private DateTime startedSavedDateTime = DateTime.MinValue;

        private void Awake() => startedSavedDateTime = GetSavedTime();

        private void OnEnable() => saveTimeCoroutine = StartCoroutine(SaveTimeCoroutine());

        private void OnDisable()
        {
            if (saveTimeCoroutine != null)
            {
                StopCoroutine(saveTimeCoroutine);
                saveTimeCoroutine = null;
            }
        }

        private IEnumerator SaveTimeCoroutine()
        {
            while (isActiveAndEnabled)
            {
                PlayerPrefs.SetString(TIME_KEY, DateTime.UtcNow.ToString(TIME_FORMAT));
                PlayerPrefs.Save();
                yield return new WaitForSeconds(saveInterval);
            }
        }

        /// <summary>
        /// Получает последнее сохраненное время в виде DateTime.
        /// Возвращает DateTime.MinValue, если время не найдено или не удалось распарсить.
        /// </summary>
        public DateTime GetSavedTime()
        {
            string savedTime = PlayerPrefs.GetString(TIME_KEY, string.Empty);
            if (string.IsNullOrEmpty(savedTime))
            {
                return DateTime.MinValue;
            }

            try
            {
                DateTime parsedTime = DateTime.ParseExact(savedTime, TIME_FORMAT, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal);
                return parsedTime;
            }
            catch (FormatException ex)
            {
                PlayerPrefs.DeleteKey(TIME_KEY);
                PlayerPrefs.Save();
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Вычисляет количество секунд между текущим временем и последним сохраненным временем.
        /// Возвращает 0, если время не найдено, произошла ошибка или разница отрицательная.
        /// </summary>
        public float GetSecondsSinceLastSave()
        {
            if (startedSavedDateTime == DateTime.MinValue)
            {
                return 0f;
            }

            try
            {
                TimeSpan timeDifference = DateTime.UtcNow - startedSavedDateTime;
                float seconds = (float)timeDifference.TotalSeconds;
                if (seconds < 0)
                {
                    PlayerPrefs.DeleteKey(TIME_KEY);
                    PlayerPrefs.Save();
                    return 0f;
                }
                return seconds;
            }
            catch (Exception ex)
            {
                PlayerPrefs.DeleteKey(TIME_KEY);
                PlayerPrefs.Save();
                return 0f;
            }
        }
    }
}