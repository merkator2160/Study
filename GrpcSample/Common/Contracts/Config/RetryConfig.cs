namespace Common.Contracts.Config
{
    public class RetryConfig
    {
        public Int32 RetryLimit { get; set; }
        public Int32 RetryDelaySec { get; set; }
    }
}