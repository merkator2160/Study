namespace Common.Contracts.Exceptions.Application
{
    public class ValueNotFoundException : ApplicationException
    {
        public ValueNotFoundException()
        {

        }
        public ValueNotFoundException(String message) : base(message)
        {

        }
        public ValueNotFoundException(String message, Exception ex) : base(message, ex)
        {

        }
    }
}