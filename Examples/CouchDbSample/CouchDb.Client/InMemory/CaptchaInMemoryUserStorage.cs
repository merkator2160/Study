using System.Collections.Concurrent;

namespace CouchDb.Client.InMemory
{
    public class CaptchaInMemoryUserStorage
    {
        private readonly ConcurrentDictionary<CaptchaUser, NewChatUser> _users = new();


        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////
        public NewChatUser[] GetAll()
        {
            return _users.Values.ToArray();
        }
        public NewChatUser Get(Int64 chatId, Int64 userId)
        {
            if (_users.TryGetValue(new CaptchaUser()
            {
                ChatId = chatId,
                UserId = userId
            }, out var newUser))
                return newUser;

            return null;
        }
        public void Add(Int64 chatId, Int64 userId, Int32 messageId, Int32 sentMessageId, String prettyUserName, Int32 answer)
        {
            var key = new CaptchaUser()
            {
                ChatId = chatId,
                UserId = userId
            };
            var newValue = new NewChatUser()
            {
                ChatId = chatId,
                UserId = userId,
                JoinDateTime = DateTimeOffset.Now,
                InviteMessageId = sentMessageId,
                JoinMessageId = messageId,
                PrettyUserName = prettyUserName,
                CorrectAnswer = answer
            };

            _users.AddOrUpdate(key, newValue, (_, _) => newValue);
        }
        public void Remove(NewChatUser user)
        {
            _users.TryRemove(new CaptchaUser()
            {
                ChatId = user.ChatId,
                UserId = user.UserId
            }, out _);
        }
    }
}