using CinemaSchedule.DataBase.Interfaces;
using CinemaSchedule.DataBase.Repositories;
using System;

namespace CinemaSchedule.DataBase
{
    public class UnitOfWork : IUniteOfWork, IDisposable
    {
        private readonly DataContext _dbContext;
        private Boolean _disposed;


        public UnitOfWork(DataContext dataContext)
        {
            _dbContext = dataContext;
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }


        // IUniteOfWork ///////////////////////////////////////////////////////////////////////////
        public ICinemaRepository Cinemas => _cinemas ?? (_cinemas = new CinemaRepository(_dbContext));
        private ICinemaRepository _cinemas;
        public IFilmRepository Films => _films ?? (_films = new FilmRepository(_dbContext));
        private IFilmRepository _films;
        public ISessionRepository Sessions => _sessions ?? (_sessions = new SessionRepository(_dbContext));
        private ISessionRepository _sessions;


        public void Commit()
        {
            _dbContext.SaveChanges();
        }


        // IDisposable ////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(Boolean disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}