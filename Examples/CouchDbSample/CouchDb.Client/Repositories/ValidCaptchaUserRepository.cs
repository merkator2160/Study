using CouchDb.Client.Models.Storage;
using CouchDB.Driver.Extensions;

namespace CouchDb.Client.Repositories
{
    public class ValidCaptchaUserRepository : CouchDbRepositoryBase<ValidCaptchaUserDb>
    {
        private readonly DataContext _context;


        public ValidCaptchaUserRepository(DataContext context) : base(context.ValidCaptchaUsers)
        {
            _context = context;
        }


        // IValidCaptchaUserRepository //////////////////////////////////////////////////////////////////
        public async Task<Boolean> ExistsAsync(Int64 chatId, Int64 userId)
        {
            var test = await _context.ValidCaptchaUsers.FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId);
            if (test == null)
                return false;

            return true;
        }
        public Task AddAsync(Int64 chatId, Int64 userId, String prettyUserName, String chatName)
        {
            return _context.ValidCaptchaUsers.AddAsync(new ValidCaptchaUserDb()
            {
                ChatId = chatId,
                UserId = userId,
                PrettyUserName = prettyUserName,
                ChatName = chatName,
                TimeStampUtc = DateTime.UtcNow
            });
        }
    }
}