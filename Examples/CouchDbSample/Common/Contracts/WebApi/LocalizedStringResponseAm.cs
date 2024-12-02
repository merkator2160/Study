namespace Common.Contracts.WebApi
{
    public class LocalizedStringResponseAm
    {
        public String Name { get; set; }
        public String Value { get; set; }
        public Boolean ResourceNotFound { get; set; }
        public String SearchedLocation { get; set; }
    }
}