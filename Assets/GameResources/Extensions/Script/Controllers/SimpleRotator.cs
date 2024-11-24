namespace Features.Extensions.Controllers
{
    using UnityEngine;

    /// <summary>
    /// Rotate game object
    /// </summary>
    public class SimpleRotator : MonoBehaviour
    {
        [SerializeField]
        protected Vector3 rotation = new Vector3(0, 0, -573);

        protected virtual void Update()
        {
            gameObject.transform.Rotate(rotation * Time.deltaTime);
        }
    }
}