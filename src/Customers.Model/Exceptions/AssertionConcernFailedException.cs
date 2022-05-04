namespace Customers.Model.Exceptions
{
    public class AssertionConcernFailedException : Exception
    {
        public AssertionConcernFailedException(string message)
            : base(message)
        {
        }
    }
}