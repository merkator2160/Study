﻿using CouchDB.Driver.Types;

namespace CouchDb.Client.Models.Storage
{
    public class ValidCaptchaUserDb : CouchDocument
    {
        public Int64 ChatId { get; set; }
        public Int64 UserId { get; set; }
        public String PrettyUserName { get; set; }
        public String ChatName { get; set; }
        public DateTime TimeStampUtc { get; set; }
    }
}