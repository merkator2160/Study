namespace Common.Contracts.Config
{
    public class CacheConfig
    {
        public Int32 DefaultExpirySec { get; set; }
        public Boolean IsCachingEnabled { get; set; }
    }
}