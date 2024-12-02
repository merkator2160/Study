using CouchDb.Client.Models.Storage;
using CouchDB.Driver.Extensions;

namespace CouchDb.Client.Repositories
{
    public class CaptchaUserRepository : CouchDbRepositoryBase<NewChatUserDb>
    {
        private readonly DataContext _context;


        public CaptchaUserRepository(DataContext context) : base(context.NewChatUsers)
        {
            _context = context;
        }


        // ICaptchaUserRepository //////////////////////////////////////////////////////////////////
        public Task<NewChatUserDb[]> GetAllAsync()
        {
            return _context.NewChatUsers.ToArrayAsync();
        }
        public Task<NewChatUserDb> GetAsync(Int64 chatId, Int64 userId)
        {
            return _context.NewChatUsers.FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId);
        }
        public Task AddAsync(Int64 chatId, Int64 userId, Int32 messageId, Int32 sentMessageId, String prettyUserName, Int32 answer)
        {
            return _context.NewChatUsers.AddOrUpdateAsync(new NewChatUserDb()
            {
                Id = Guid.NewGuid().ToString(),
                ChatId = chatId,
                UserId = userId,
                JoinDateTime = DateTimeOffset.Now,
                InviteMessageId = sentMessageId,
                JoinMessageId = messageId,
                PrettyUserName = prettyUserName,
                CorrectAnswer = answer
            });
        }
        public Task RemoveAsync(NewChatUserDb user)
        {
            return _context.NewChatUsers.RemoveAsync(user);
        }
    }
}