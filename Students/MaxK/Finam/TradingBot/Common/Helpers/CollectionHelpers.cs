namespace Common.Helpers
{
    public static class CollectionHelpers
    {
        public static T[][] SplitOnChunks<T>(this IEnumerable<T> array, Int32 chunkSize = 100)
        {
            return array
                .Select((s, i) => new { Value = s, Index = i })
                .GroupBy(x => x.Index / chunkSize)
                .Select(grp => grp.Select(x => x.Value).ToArray())
                .ToArray();
        }
    }
}