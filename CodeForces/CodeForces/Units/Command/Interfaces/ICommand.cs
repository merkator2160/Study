namespace CodeForces.Units.CommandUnit.Interfaces
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}