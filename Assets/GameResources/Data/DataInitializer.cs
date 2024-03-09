namespace Features.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data initializaer
    /// </summary>
    public class DataInitializer : MonoBehaviour
    {
        [SerializeField]
        protected List<InitableData> initableDatas = new List<InitableData>();

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitData();
        }

        protected virtual void InitData()
            => initableDatas.ForEach(data => data.Init());

        protected virtual void DisposeData()
            => initableDatas.ForEach(data => data.Dispose());

        protected virtual void OnDestroy()
            => DisposeData();
    }
}