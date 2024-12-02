using CouchDB.Driver.Types;

namespace CouchDb.Client.Models.Storage
{
    public class SmmPostSampleDb : CouchDocument
    {
        public Int64 ChatId { get; init; }
        public Int64 UserId { get; init; }

        public String PrettyUserName { get; set; }
        public String ChatName { get; set; }
        public DateTime TimeStampUtc { get; set; }

        public String MessageText { get; set; }
    }
}