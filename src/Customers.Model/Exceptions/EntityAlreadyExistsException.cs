namespace Customers.Model.Exceptions
{
    public class EntityAlreadyExistsException : OperationException
    {
        public EntityAlreadyExistsException(string identifier, string entityType)
            : base($"An entity of type '{entityType}' with identifier '{identifier}' already exists")
        {
        }
    }
}