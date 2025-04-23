namespace Features.AdsControl
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class RewardedInterstitialInteractableController : MonoBehaviour
    {
        [SerializeField]
        private Selectable _selectable = default;
        private Coroutine _coroutine = default;

        private void OnEnable()
        {
            if (RewardedInterstitialController.Instance != null)
            {
                _coroutine = StartCoroutine(ReadyCheck());
            }
        }

        private IEnumerator ReadyCheck()
        {
            while (isActiveAndEnabled && RewardedInterstitialController.Instance != null)
            {
                _selectable.interactable = RewardedInterstitialController.Instance.IsReady;
                yield return null;
            }
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
}
