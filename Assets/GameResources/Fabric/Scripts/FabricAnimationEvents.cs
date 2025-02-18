namespace Features.Fabric
{
    using System;
    using UnityEngine;

    public class FabricAnimationEvents : MonoBehaviour
    {
        public event Action onHandDown = delegate { };
        public event Action onHandUp = delegate { };
        public event Action onConsumeEnd = delegate { };

        public bool IsHandDown { get; protected set; } = false;

        protected virtual void NotifyOnHandDown()
        {
            IsHandDown = false;
            onHandDown();
        }

        protected virtual void NotifyOnHandUp()
        {
            IsHandDown = true;
            onHandUp();
        }

        protected virtual void NotifyOnConsumeEnd() => onConsumeEnd();
    }
}
