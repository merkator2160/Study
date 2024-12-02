using CouchDb.Client.InMemory;
using CouchDb.Client.Models;
using CouchDb.Client.Repositories;

namespace CouchDb.Client
{
    public class RepositoryBundle
    {
        public RepositoryBundle(
            DataContext context,
            CaptchaUserRepository captchaUsers,
            ValidCaptchaUserRepository validCaptchaUserRepository,
            CaptchaInMemoryUserStorage captchaInMemoryUsersStorage,
            SmmPostSampleRepository smmPostSampleRepository)
        {
            Context = context;
            CaptchaUsers = captchaUsers;
            ValidCaptchaUsers = validCaptchaUserRepository;
            CaptchaInMemoryUsers = captchaInMemoryUsersStorage;
            SmmPostSamples = smmPostSampleRepository;
        }


        // IRepositoryBundle //////////////////////////////////////////////////////////////////////
        public DataContext Context { get; init; }
        public CaptchaUserRepository CaptchaUsers { get; init; }
        public ValidCaptchaUserRepository ValidCaptchaUsers { get; init; }
        public CaptchaInMemoryUserStorage CaptchaInMemoryUsers { get; init; }
        public SmmPostSampleRepository SmmPostSamples { get; init; }


        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////
        public async Task<StatisticDb> GetStatisticAsync()
        {
            return new StatisticDb()
            {
                ValidCaptchaUsersCount = await ValidCaptchaUsers.CountAsync(),
                SmmPostSamplesCount = await SmmPostSamples.CountAsync()
            };
        }
    }
}