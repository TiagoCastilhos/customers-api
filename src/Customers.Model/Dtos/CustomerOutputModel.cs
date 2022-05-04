namespace Customers.Model.Dtos
{
    public sealed class CustomerOutputModel
    {
        public Guid Id{ get; init; }
        public string Email { get; init; }
        public string Name { get; init; }
    }
}
