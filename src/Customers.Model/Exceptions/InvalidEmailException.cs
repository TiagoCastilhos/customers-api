namespace Customers.Model.Exceptions
{
    public sealed class InvalidEmailException : AssertionConcernFailedException
    {
        public InvalidEmailException(string email)
            : base($"Provided argument is not a valid email. Argument: {email}")
        {
        }
    }
}