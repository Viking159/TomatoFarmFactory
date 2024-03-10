namespace Features.Extensions.DontDestroy
{
    using UnityEngine;

    /// <summary>
    /// Dont destory object on load controller
    /// </summary>
    public class DontDestroyObject : MonoBehaviour
    {
        protected virtual void Awake()
            => DontDestroyOnLoad(gameObject);
    }
}