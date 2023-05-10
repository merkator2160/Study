using MongoDB.Driver;

namespace SerializationTask.Database.Extensions
{
    public static class CursorExtensions
    {
        public static void ForEach<T>(this IAsyncCursor<T> cursor, Action<T> processor)
        {
            using (cursor)
            {
                while (cursor.MoveNext())
                {
                    foreach (var x in cursor.Current)
                    {
                        processor(x);
                    }
                }
            }
        }
        public static IEnumerable<TResult> ForEach<T, TResult>(this IAsyncCursor<T> cursor, Func<T, TResult> processor)
        {
            using (cursor)
            {
                while (cursor.MoveNext())
                {
                    foreach (var x in cursor.Current)
                    {
                        yield return processor(x);
                    }
                }
            }
        }
        public static T[] ToArray<T>(this IAsyncCursorSource<T> source)
        {
            return source.ToList().ToArray();
        }
        public static async Task<T[]> ToArrayAsync<T>(this IAsyncCursorSource<T> source)
        {
            return (await source.ToListAsync()).ToArray();
        }
        public static T[] ToArray<T>(this IAsyncCursor<T> source)
        {
            return source.ToList().ToArray();
        }
        public static async Task<T[]> ToArrayAsync<T>(this IAsyncCursor<T> source)
        {
            return (await source.ToListAsync()).ToArray();
        }
        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IAsyncCursor<T> cursor)
        {
            while (await cursor.MoveNextAsync())
            {
                foreach (var current in cursor.Current)
                {
                    yield return current;
                }
            }
        }
    }
}