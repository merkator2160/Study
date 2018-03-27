using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestService.Common.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() == 0;
        }
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null || action == null) return;

            foreach (T element in list)
            {
                action(element);
            }
        }
        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            if (list == null || action == null) return;

            int index = 0;
            foreach (T element in list)
            {
                action(element, index);
                index++;
            }
        }
        /// <summary>
        /// Finds the index in the collection where the predicate evaluates to true.
        /// 
        /// Returns -1 if no matching item found
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="predicate">Function to evaluate</param>
        /// <returns>Index where predicate is true, or -1 if not found.</returns>
        public static int IndexWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                TSource obj = enumerator.Current;
                if (predicate(obj))
                    return index;
                index++;
            }
            return -1;
        }
        /// <summary>
        /// Finds the index in the collection where the predicate evaluates to true.
        /// 
        /// Returns -1 if no matching item found
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="predicate">Function to evaluate</param>
        /// <returns>Index where predicate is true, or -1 if not found.</returns>
        public static int IndexWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                TSource obj = enumerator.Current;
                if (predicate(obj, index))
                    return index;
                index++;
            }
            return -1;
        }
        public class DelegateComparer<T> : IEqualityComparer<T>
        {
            private Func<T, T, bool> _equals;
            private Func<T, int> _hashCode;
            public DelegateComparer(Func<T, T, bool> equals, Func<T, int> hashCode)
            {
                _equals = equals;
                _hashCode = hashCode;
            }
            public bool Equals(T x, T y)
            {
                return _equals(x, y);
            }

            public int GetHashCode(T obj)
            {
                if (_hashCode != null)
                    return _hashCode(obj);
                return obj.GetHashCode();
            }
        }
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> items, Func<T, T, bool> equals, Func<T, int> hashCode)
        {
            return items.Distinct(new DelegateComparer<T>(equals, hashCode));
        }
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> items, Func<T, T, bool> equals)
        {
            return items.Distinct(new DelegateComparer<T>(equals, null));
        }
    }
}
