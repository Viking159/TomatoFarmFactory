namespace Features.Extensions.DontDestroy
{
    using UnityEngine;

    /// <summary>
    /// Dont destory object on load controller
    /// </summary>
    public class DontDestroyObject : MonoBehaviour
    {
        protected static DontDestroyObject instance = default;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}