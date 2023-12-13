namespace Common.Interfaces
{
    public interface ICache
    {
        Boolean IsCachingEnabled { get; }

        void Set<T>(String key, T obj);
        void SetString(String key, String str);

        T Get<T>(String key);
        String GetString(String key);

        Boolean TryGet<T>(String key, out T obj);
        Boolean TryGetString(String key, out String str);

        void Remove(String key);
    }
}