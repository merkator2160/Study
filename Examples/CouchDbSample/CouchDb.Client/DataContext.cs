using CouchDb.Client.Models.Config;
using CouchDb.Client.Models.Storage;
using CouchDB.Driver;

namespace CouchDb.Client
{
    // https://github.com/matteobortolazzo/couchdb-net
    public class DataContext
    {
        private readonly CouchClient _client;
        private readonly CouchDbConfig _config;


        public DataContext(CouchClient client, CouchDbConfig config)
        {
            _client = client;
            _config = config;
        }


        // PROPERTIES //////////////////////////////////////////////////////////////////////////////
        public ICouchClient Client => _client;


        // ENTITIES ///////////////////////////////////////////////////////////////////////////////
        public ICouchDatabase<NewChatUserDb> NewChatUsers => _client.GetDatabase<NewChatUserDb>(_config.DbName, "NewChatUsers");
        public ICouchDatabase<ValidCaptchaUserDb> ValidCaptchaUsers => _client.GetDatabase<ValidCaptchaUserDb>(_config.DbName, "validCaptchaUsers");
        public ICouchDatabase<SmmPostSampleDb> SmmPostSample => _client.GetDatabase<SmmPostSampleDb>(_config.DbName, "SmmPostSignature");


        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////
        public async Task CheckDatabasesAsync()
        {
            await _client.GetOrCreateDatabaseAsync<ValidCaptchaUserDb>(_config.DbName);
        }
        public async Task CreateIndexesAsync()
        {
            await ValidCaptchaUsers.CreateIndexAsync("valid_captcha_users_index", p => p
                    .IndexBy(r => r.ChatId)
                    .ThenBy(r => r.UserId));
        }
    }
}