namespace Features.Extensions.BaseDataTypes
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generic List extension
    /// </summary>
    public static class ListExtensions
    {
        private const string BEFORE_LAST_OPERATION_EXCEPTION = "Index out of range";
        private const int EMPTY_LIST_COUNT = 0;
        private const int PREVIOUS_INDEX = 1;
        private const int BEFORE_LAST_INDEX = 2;

        public static int IndexOf<T>(this IReadOnlyList<T> list, T element)
            where T : UnityEngine.Object
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetInstanceID() == element.GetInstanceID())
                {
                    return i;
                }
            }
            return -1;
        }

        public static T BeforeLast<T>(this IReadOnlyList<T> list)
        {
            if (list.IsNullOrEmpty() || list.Count <= PREVIOUS_INDEX)
            {
                throw new InvalidOperationException(BEFORE_LAST_OPERATION_EXCEPTION); 
            }
            return list[list.Count - BEFORE_LAST_INDEX];
        }

        public static T BeforeLastOrDefault<T>(this IReadOnlyList<T> list)
        {
            if (list.IsNullOrEmpty() || list.Count <= PREVIOUS_INDEX)
            {
                return default;
            }
            return list[list.Count - BEFORE_LAST_INDEX];
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
            => list == null || list.Count() == EMPTY_LIST_COUNT;

        public static bool IsValidIndex<T>(this IEnumerable<T> list, int index)
            => list != null && index >= EMPTY_LIST_COUNT && index < list.Count();
    }
}

