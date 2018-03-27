namespace CodeForces.Units.Command.Commands
{
    public abstract class CommandBase
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}