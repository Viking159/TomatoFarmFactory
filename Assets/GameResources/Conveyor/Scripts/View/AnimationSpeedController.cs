namespace Features.Conveyor
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class AnimationSpeedController : MonoBehaviour
    {
        [SerializeField]
        protected ConveyorElement conveyorElement = default;
        [SerializeField]
        protected float ratio = 0.5f;
        protected Animator animator = default;

        protected virtual void Awake() => animator = GetComponent<Animator>();

        protected virtual void OnEnable()
        {
            SetAnimationSpeed();
            conveyorElement.onSpeedValueChange += SetAnimationSpeed;
        }

        protected virtual void SetAnimationSpeed() => animator.speed = conveyorElement.Speed * ratio;

        protected virtual void OnDisable() => conveyorElement.onSpeedValueChange -= SetAnimationSpeed;
    }
}
