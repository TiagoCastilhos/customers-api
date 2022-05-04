using Customers.Model.Exceptions;

namespace Customers.Model
{
    public static class AssertionConcern
    {
        public static void AssertArgumentNotNullOrEmpty(string parameter, string paramName)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new AssertionArgumentNullException(paramName);
        }

        public static void AssertArgumentNotNull<T>(T argument, string paramName) where T : class
        {
            if (argument == null)
                throw new AssertionArgumentNullException(paramName);
        }

        public static void AssertArgumentIsEmail(string email)
        {
            if (email.Count(c => c == '@') != 1)
                throw new InvalidEmailException(email);
        }
    }
}
