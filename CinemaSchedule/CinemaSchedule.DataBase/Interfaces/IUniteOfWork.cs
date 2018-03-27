namespace CinemaSchedule.DataBase.Interfaces
{
    public interface IUniteOfWork
    {
        ICinemaRepository Cinemas { get; }
        IFilmRepository Films { get; }
        ISessionRepository Sessions { get; }

        void Commit();
    }
}