namespace Features.Extensions.BaseDataTypes
{
    using UnityEngine;

    /// <summary>
    /// Extensions for UnityEngine.Vector2 and UnityEngine.Vector3
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Get closest point to current
        /// </summary>
        /// <param name="p">current point</param>
        /// <param name="p1">point 1</param>
        /// <param name="p2">point 2</param>
        /// <returns>Closest point</returns>
        public static Vector2 ClosestPoint(this Vector2 p, Vector2 p1, Vector2 p2)
            => Vector2.Distance(p, p1) < Vector2.Distance(p, p2) ? p1: p2;

        /// <summary>
        /// Get closest point to current
        /// </summary>
        /// <param name="p">current point</param>
        /// <param name="p1">point 1</param>
        /// <param name="p2">point 2</param>
        /// <returns>Closest point</returns>
        public static Vector3 ClosestPoint(this Vector3 p, Vector3 p1, Vector3 p2)
            => Vector3.Distance(p, p1) < Vector3.Distance(p, p2) ? p1 : p2;
    }
}

