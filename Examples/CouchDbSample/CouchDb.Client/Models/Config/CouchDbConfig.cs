namespace CouchDb.Client.Models.Config
{
    public record CouchDbConfig
    {
        public String Url { get; init; }
        public String Login { get; init; }
        public String Password { get; init; }
        public String DbName { get; init; }
    }
}