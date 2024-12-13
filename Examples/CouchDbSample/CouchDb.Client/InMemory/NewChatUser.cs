﻿namespace CouchDb.Client.InMemory
{
    public record NewChatUser()
    {
        public Int64 ChatId { get; init; }
        public Int64 UserId { get; init; }
        public DateTimeOffset JoinDateTime { get; init; }
        public Int32 InviteMessageId { get; init; }
        public Int32 JoinMessageId { get; init; }
        public String PrettyUserName { get; init; }
        public Int32 CorrectAnswer { get; init; }
    }
}