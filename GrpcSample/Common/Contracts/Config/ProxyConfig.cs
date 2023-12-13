namespace Common.Contracts.Config
{
    public class ProxyConfig
    {
        public Boolean UseProxy { get; set; }
        public String SocksProxy { get; set; }
        public Int32 SocksVersion { get; set; }
        public String Description { get; set; }
    }
}