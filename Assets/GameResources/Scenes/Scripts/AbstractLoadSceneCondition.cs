namespace Features.Scenes
{
    using UnityEngine;

    /// <summary>
    /// Abstract load scene condition
    /// </summary>
    public abstract class AbstractLoadSceneCondition : MonoBehaviour
    {
        /// <summary>
        /// Is valid condition
        /// </summary>
        public bool IsValidCondition => isValidCondition;
        protected bool isValidCondition = false;

        /// <summary>
        /// Init condition
        /// </summary>
        public abstract void InitCondition();
    }
}