namespace Features.Scenes
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Features.Data.BaseContainerData;

    /// <summary>
    /// Load next scene controller
    /// </summary>
    public class LoadNextSceneController : MonoBehaviour
    {
        [SerializeField]
        protected StringNotifyData nextSceneContainer = default;
        [SerializeField]
        protected List<AbstractLoadSceneCondition> conditions = new List<AbstractLoadSceneCondition>();
        [SerializeField, Min(0)]
        protected float minAwaitTime = 1.5f;

        protected AsyncOperation loadOperation = default;
        protected Coroutine conditionsCoroutine = default;
        protected int initedControllers = 0;
        protected string sceneName = string.Empty;

        protected float startTime = default;

        protected virtual void OnEnable()
        {
            startTime = Time.time;
            sceneName = SceneManager.GetActiveScene().name;
            conditions.ForEach(condition => condition.InitCondition());
            conditionsCoroutine = StartCoroutine(WaitConditions());
        }

        protected virtual IEnumerator WaitConditions()
        {
            yield return new WaitForSeconds(minAwaitTime);
            while (conditions.Exists(condition => condition.IsValidCondition == false))
            {
                yield return null;
            }
            LoadNextScene();
        }

        protected virtual void LoadNextScene()
        {
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

        protected virtual  void OnDisable()
        {
            if (conditionsCoroutine != null)
            {
                StopCoroutine(conditionsCoroutine);
                conditionsCoroutine = null;
            }
            if (loadOperation != null)
            {
                loadOperation.completed -= OnSceneLoaded;
            }
        }
    }
}