using CodeForces.Units.Command.Commands;

namespace CodeForces.Units.Command
{
	public class Invoker
	{
		private CommandBase _command;


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void SetCommand(CommandBase command)
		{
			_command = command;
		}
		public void Run()
		{
			_command.Execute();
		}
		public void Cancel()
		{
			_command.Undo();
		}
	}
}