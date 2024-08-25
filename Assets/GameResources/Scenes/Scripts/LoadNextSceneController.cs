namespace Features.Scenes
{
    using Features.Data.BaseContainerData;
    using Features.SaveSystem;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Load next scene controller
    /// </summary>
    public class LoadNextSceneController : MonoBehaviour
    {
        [SerializeField]
        protected StringNotifyData nextSceneContainer = default;
        [SerializeField]
        protected List<AbstractDataFileController> dataControllers = new List<AbstractDataFileController>();
        [SerializeField, Min(0)]
        protected float minAwaitTime = 1.5f;

        protected AsyncOperation loadOperation = default;
        protected int initedControllers = 0;
        protected string sceneName = string.Empty;

        protected float startTime = default;

        protected virtual void OnEnable()
        {
            startTime = Time.time;
            sceneName = SceneManager.GetActiveScene().name;
            ListenControllers();
            dataControllers.ForEach(controller => controller.LoadData());
        }

        protected virtual void ListenControllers()
        {
            for (int i = 0; i < dataControllers.Count; i++)
            {
                dataControllers[i].onDataInited += IncrementInitedControllersCount;
            }
        }

        protected virtual void StopListenControllers()
        {
            for (int i = 0; i < dataControllers.Count; i++)
            {
                dataControllers[i].onDataInited -= IncrementInitedControllersCount;
            }
        }

        protected virtual void IncrementInitedControllersCount()
        {
            initedControllers++;
            if (TryLoadNextScene())
            {
                StopListenControllers();
            }
        }

        protected virtual bool TryLoadNextScene()
        {
            if (initedControllers >= dataControllers.Count)
            {
                LoadNextScene();
                return true;
            }
            return false;
        }

        protected virtual async void LoadNextScene()
        {
            float timeLeft = minAwaitTime - (Time.time - startTime);
            if (timeLeft > 0)
            {
                await Task.Delay(TimeSpan.FromSeconds(timeLeft));
            }
            loadOperation = SceneManager.LoadSceneAsync(nextSceneContainer.DataValue, LoadSceneMode.Additive);
            if (loadOperation != null)
            {
                loadOperation.completed += OnSceneLoaded;
            }
        }

        protected virtual void OnSceneLoaded(AsyncOperation obj)
        {
            loadOperation.completed -= OnSceneLoaded;
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private void OnDisable()
        {
            if (loadOperation != null)
            {
                loadOperation.completed -= OnSceneLoaded;
            }
            StopListenControllers();
        }
    }
}