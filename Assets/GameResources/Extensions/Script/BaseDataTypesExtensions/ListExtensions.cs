namespace Features.Extensions.BaseDataTypes
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// Generic List extension
    /// </summary>
    public static class ListExtensions
    {
        private const int EMPTY_LIST_COUNT = 0;
        private const int PREVIOUS_INDEX = 1;
        private const int BEFORE_LAST_INDEX = 2;

        public static T BeforeLast<T>(this List<T> list)
        {
            if (list.IsNullOrEmpty() || list.Count <= PREVIOUS_INDEX)
            {
                return default;
            }
            return list[list.Count - BEFORE_LAST_INDEX];
        }

        public static T Last<T>(this List<T> list)
        {
            if (list.IsNullOrEmpty())
            {
                return default;
            }
            return list[list.Count - PREVIOUS_INDEX];
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
            => list == null || list.Count == EMPTY_LIST_COUNT;

        public static bool IsValidIndex<T>(this List<T> list, int index)
            => list != null && index >= EMPTY_LIST_COUNT && index < list.Count;
    }
}

