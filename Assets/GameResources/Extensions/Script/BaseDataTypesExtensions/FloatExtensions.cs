namespace Features.Extensions.BaseDataTypes
{
    using UnityEngine;

    /// <summary>
    /// Float extensions
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Approximately compare float numbers with epsilon
        /// </summary>
        /// <returns>(1) - greater; (-1) - less; (0) - queals</returns>
        public static int ApproximatelyCompare(this float val1, float val2, float epsilon = 0.01f)
        {
            if ((val1 >= val2 ? val1 - val2 : val2 - val1) > epsilon)
            {
                return val1 > val2 ? 1 : -1;
            }
            return 0;
        }

        /// <summary>
        /// Is vectors approximately equals
        /// </summary>
        public static bool ApproximatelyEqualsVectors(this Vector3 v1, Vector3 v2, float epsilon = 0.01f)
            => ApproximatelyEqualsVectors((Vector2)v1, (Vector2)v2, epsilon)
            && v1.z.ApproximatelyCompare(v2.z) == 0;

        public static bool ApproximatelyEqualsVectors(this Vector2 v1, Vector2 v2, float epsilon = 0.01f)
            => v1.x.ApproximatelyCompare(v2.x, epsilon) == 0
            && v1.y.ApproximatelyCompare(v2.y, epsilon) == 0;
    }
}