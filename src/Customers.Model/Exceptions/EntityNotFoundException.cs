namespace Customers.Model.Exceptions
{
    public class EntityNotFoundException : OperationException
    {
        public EntityNotFoundException(string identifier, string entityType)
            : base($"An entity of type '{entityType}' with identifier '{identifier}' does not exist")
        {
        }
    }
}