namespace Features.Conveyor
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Conveyor rider collision with other riders collider activity controller
    /// </summary>
    public class ConveyorRiderTriggerController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject trigger = default;
        protected ConveyorRider conveyorRider = default;
        protected Coroutine coroutine = default;

        protected virtual void Awake() => conveyorRider = GetComponentInParent<ConveyorRider>();

        protected virtual void OnEnable() => coroutine = StartCoroutine(LateTriggerActivate());

        protected virtual IEnumerator LateTriggerActivate()
        {
            while (isActiveAndEnabled)
            {
                while (!conveyorRider.IsPathInited)
                {
                    yield return null;
                }
                trigger.SetActive(true);
                yield break;
            }
        }

        protected virtual void OnDisable()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            trigger.SetActive(false);
        }
    }
}