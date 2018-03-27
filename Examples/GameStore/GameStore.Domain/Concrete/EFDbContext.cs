using GameStore.Domain.Entities;
using System.Data.Entity;


namespace GameStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public DbSet<Game> Games { get; set; }
    }
}
