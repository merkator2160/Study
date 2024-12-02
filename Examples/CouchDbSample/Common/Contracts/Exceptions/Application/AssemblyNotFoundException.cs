namespace Common.Contracts.Exceptions.Application
{
    public class AssemblyNotFoundException : ApplicationException
    {
        public AssemblyNotFoundException()
        {

        }
        public AssemblyNotFoundException(String message) : base(message)
        {

        }
        public AssemblyNotFoundException(String message, Exception ex) : base(message, ex)
        {

        }
    }
}