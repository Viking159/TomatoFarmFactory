namespace Features.Extensions
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Base game object switcher
    /// </summary>
    public class BaseObjectSwitcher : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> enableObjects = new List<GameObject>();
        [SerializeField]
        protected List<GameObject> disableObjects = new List<GameObject>();

        protected bool prevEnableValue = false;
        protected bool isFirstTime = true;

        protected virtual void SetObjects(bool isEnable)
        {
            if (prevEnableValue != isEnable || isFirstTime)
            {
                SwitchObjects(enableObjects, isEnable);
                SwitchObjects(disableObjects, !isEnable);
                prevEnableValue = isEnable;
                isFirstTime = false;
            }
        }

        private void SwitchObjects(List<GameObject> objs, bool isEnable)
        {
            foreach (GameObject obj in objs)
            {
                if (obj != null)
                {
                    obj.SetActive(isEnable);
                }
            }
        }
    }
}