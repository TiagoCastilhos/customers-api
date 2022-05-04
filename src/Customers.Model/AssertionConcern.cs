using Customers.Model.Exceptions;
using ArgumentNullException = Customers.Model.Exceptions.ArgumentNullException;

namespace Customers.Model
{
    public static class AssertionConcern
    {
        public static void AssertArgumentNotNullOrEmpty(string parameter, string paramName)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException(paramName);
        }

        public static void AssertArgumentNotNull<T>(T argument, string paramName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(paramName);
        }

        public static void AssertArgumentIsEmail(string email)
        {
            if (email.Count(c => c == '@') != 1)
                throw new InvalidEmailException(email);
        }
    }
}
