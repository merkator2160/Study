namespace CouchDb.Client.InMemory
{
    public record struct CaptchaUser()
    {
        public Int64 ChatId { get; set; }
        public Int64 UserId { get; set; }
    }
}
