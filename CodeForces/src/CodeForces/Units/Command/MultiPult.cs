using CodeForces.Units.Command.Commands;
using CodeForces.Units.Command.Interfaces;
using System;
using System.Collections.Generic;

namespace CodeForces.Units.Command
{
	public class MultiPult
	{
		private readonly ICommand[] _buttons;
		private readonly Stack<ICommand> _commandsHistory;

		public MultiPult()
		{
			_buttons = new ICommand[2];
			for(var i = 0; i < _buttons.Length; i++)
			{
				_buttons[i] = new EmptyCommand();
			}
			_commandsHistory = new Stack<ICommand>();
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void SetCommand(Int32 number, ICommand com)
		{
			_buttons[number] = com;
		}
		public void PressButton(Int32 number)
		{
			_buttons[number].Execute();
			_commandsHistory.Push(_buttons[number]);    // добавляем выполненную команду в историю команд
		}
		public void PressUndoButton()
		{
			if(_commandsHistory.Count > 0)
			{
				var undoCommand = _commandsHistory.Pop();
				undoCommand.Undo();
			}
		}
	}
}