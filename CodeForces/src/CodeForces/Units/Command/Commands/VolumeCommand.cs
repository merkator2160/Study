using CodeForces.Units.CommandUnit;
using CodeForces.Units.CommandUnit.Interfaces;

namespace CodeForces.Units.Command.Commands
{
    public class VolumeCommand : ICommand
    {
        readonly Volume _volume;


        public VolumeCommand(Volume volume)
        {
            _volume = volume;
        }


        // ICommand ///////////////////////////////////////////////////////////////////////////////
        public void Execute()
        {
            _volume.RaiseLevel();
        }
        public void Undo()
        {
            _volume.DropLevel();
        }
    }
}