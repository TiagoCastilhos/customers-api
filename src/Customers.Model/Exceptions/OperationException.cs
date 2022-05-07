namespace Customers.Model.Exceptions
{
    public class OperationException : Exception
    {
        public OperationException(string message)
            : base(message)
        {
        }
    }
}