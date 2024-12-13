using CouchDB.Driver.Types;

namespace Sandbox.Units.CouchDb.Models
{
    public class TestOneDb : CouchDocument
    {
        public Int64 ChatId { get; set; }
        public Int64 UserId { get; set; }
    }
}