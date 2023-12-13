namespace Common.Contracts.Exceptions.BusinessLogic
{
    public class BusinessLogicException : ApplicationException
    {
        public BusinessLogicException()
        {

        }
        public BusinessLogicException(String message) : base(message)
        {

        }
        public BusinessLogicException(String message, Exception ex) : base(message, ex)
        {

        }
    }
}