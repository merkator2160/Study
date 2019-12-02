using CodeForces.Units.Command.Interfaces;

namespace CodeForces.Units.Command.Commands
{
	public class TvSetCommand : ICommand
	{
		private readonly TvSet _tvSet;


		public TvSetCommand(TvSet tvSet)
		{
			_tvSet = tvSet;
		}


		// ICommand ///////////////////////////////////////////////////////////////////////////////
		public void Execute()
		{
			_tvSet.On();
		}
		public void Undo()
		{
			_tvSet.Off();
		}
	}
}