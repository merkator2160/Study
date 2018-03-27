﻿using CodeForces.Units.Command.Commands;
using CodeForces.Units.CommandUnit.Interfaces;

namespace CodeForces.Units.CommandUnit
{
    public class Pult
    {
        private ICommand _command;


        public Pult()
        {
            _command = new EmptyCommand();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void SetCommand(ICommand command)
        {
            _command = command;
        }
        public void PressButton()
        {
            _command.Execute();
        }
        public void PressUndo()
        {
            _command.Undo();
        }
    }
}