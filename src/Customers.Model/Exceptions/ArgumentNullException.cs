namespace Customers.Model.Exceptions
{
    public sealed class ArgumentNullException : AssertionConcernFailedException
    {
        public ArgumentNullException(string paramName)
            : base($"Parameter {paramName} must be provided")
        {
        }
    }
}