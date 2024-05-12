namespace Features.InfoBox
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Info box controller
    /// </summary>
    public class InfoBox : MonoBehaviour
    {
        /// <summary>
        /// Box close event
        /// </summary>
        public event Action onBoxClose = delegate { };

        /// <summary>
        /// Close box method
        /// </summary>
        public virtual void CloseBox()
            => onBoxClose();
    }
}