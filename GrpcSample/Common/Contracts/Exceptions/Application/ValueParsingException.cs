namespace Common.Contracts.Exceptions.Application
{
    public class ValueParsingException : ApplicationException
    {
        public ValueParsingException()
        {

        }
        public ValueParsingException(String message) : base(message)
        {

        }
        public ValueParsingException(String message, Exception ex) : base(message, ex)
        {

        }
    }
}