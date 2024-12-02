using CouchDb.Client.Models.Storage;

namespace CouchDb.Client.Repositories
{
    public class SmmPostSampleRepository : CouchDbRepositoryBase<SmmPostSampleDb>
    {
        private readonly DataContext _context;


        public SmmPostSampleRepository(DataContext context) : base(context.SmmPostSample)
        {
            _context = context;
        }


        // ICaptchaUserRepository //////////////////////////////////////////////////////////////////
    }
}