namespace Common.Contracts.Exceptions.Application
{
    public class RetryException : ApplicationException
    {
        public RetryException()
        {

        }
        public RetryException(String message) : base(message)
        {

        }
        public RetryException(String message, Exception ex) : base(message, ex)
        {

        }
    }
}