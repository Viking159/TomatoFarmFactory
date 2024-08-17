namespace Features.DevelopmentBuild
{
    using UnityEngine;

    /// <summary>
    /// DevelopmentBuildGameObject
    /// </summary>
    public sealed class DevelopmentBuildGameObject : MonoBehaviour
    {
        private void Awake()
        {
#if !(DEVELOPMENT_BUILD || UNITY_EDITOR)
            Destroy(gameObject);
#endif
        }
    }
}