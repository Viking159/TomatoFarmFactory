namespace Features.Temp
{
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public class Temp : MonoBehaviour
    {
        [SerializeField]
        private bool _isTriggering = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("in");
            _isTriggering = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("out");
            _isTriggering = false;
        }

    }
}