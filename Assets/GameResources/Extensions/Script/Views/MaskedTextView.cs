namespace Features.Extensions.View
{
    using UnityEngine;

    /// <summary>
    /// Text view with mask
    /// </summary>
    public class MaskedTextView : BaseTextView
    {
        [SerializeField]
        protected string mask = "{0}";

        protected virtual void SetView(params object[] strings)
            => base.SetView(string.Format(mask, strings));
    }
}


