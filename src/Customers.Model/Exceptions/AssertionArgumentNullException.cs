namespace Customers.Model.Exceptions
{
    public sealed class AssertionArgumentNullException : AssertionConcernFailedException
    {
        public AssertionArgumentNullException(string paramName)
            : base($"Parameter {paramName} must be provided")
        {
        }
    }
}