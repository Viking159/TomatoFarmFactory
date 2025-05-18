namespace Features.Extensions.DoTween
{
    using DG.Tweening;
    using UnityEngine;

    public class DOTweenCleaner : MonoBehaviour
    {
        private void OnDisable() => DOTween.KillAll();
    }
}