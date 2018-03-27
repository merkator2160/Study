using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections.Generic;


namespace GameStore.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        private EFDbContext _context = new EFDbContext();



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public IEnumerable<Game> Games
        {
            get { return _context.Games; }
        }
    }
}
