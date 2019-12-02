using CodeForces.Units.CommandUnit;
using CodeForces.Units.CommandUnit.Interfaces;
using System;

namespace CodeForces.Units.Command.Commands
{
    public class MicrowaveCommand : ICommand
    {
        private readonly Microwave _microwave;
        private readonly Int32 _time;


        public MicrowaveCommand(Microwave microwave, Int32 time)
        {
            _microwave = microwave;
            _time = time;
        }


        // ICommand ///////////////////////////////////////////////////////////////////////////////
        public void Execute()
        {
            _microwave.StartCooking(_time);
            _microwave.StopCooking();
        }
        public void Undo()
        {
            _microwave.StopCooking();
        }
    }
}