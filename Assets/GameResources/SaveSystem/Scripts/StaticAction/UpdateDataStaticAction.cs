namespace Features.SaveSystem
{
    using System;

    /// <summary>
    /// Update data static action controller
    /// </summary>
    public static class UpdateDataStaticAction
    {
        /// <summary>
        /// Update data require event
        /// </summary>
        public static event Action onUpdateRequire = delegate { };

        /// <summary>
        /// Notify
        /// </summary>
        public static void NotifyUpdateRequire() => onUpdateRequire();
    }
}