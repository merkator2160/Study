namespace CodeForces.Units.Command.Interfaces
{
	public interface ICommand
	{
		void Execute();
		void Undo();
	}
}