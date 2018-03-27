using CodeForces.Units.CommandUnit;

namespace CodeForces.Units.Command.Commands
{
    public class ConcreteCommand : CommandBase
    {
        readonly Receiver _receiver;


        public ConcreteCommand(Receiver receiver)
        {
            _receiver = receiver;
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public override void Execute()
        {
            _receiver.Operaiton();
        }
        public override void Undo()
        {

        }
    }
}