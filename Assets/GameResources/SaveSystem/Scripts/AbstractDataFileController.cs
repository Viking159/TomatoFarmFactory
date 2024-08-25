namespace Features.SaveSystem
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Absctract file controller
    /// </summary>
    public abstract class AbstractDataFileController: MonoBehaviour
    {
        protected const string JSON_FILE = "{0}.json";

        /// <summary>
        /// Data init event
        /// </summary>
        public event Action onDataInited = delegate { };

        /// <summary>
        /// Is data inited
        /// </summary>
        public bool IsInited { get; protected set; } = false;

        /// <summary>
        /// Load data
        /// </summary>
        public abstract void LoadData();

        protected virtual void NotifyOnDataInit() => onDataInited();
    }
}