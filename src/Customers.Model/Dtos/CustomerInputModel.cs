namespace Customers.Model.Dtos
{
    public sealed class CustomerInputModel
    {
        public string Email { get; }
        public string Name { get; }

        public CustomerInputModel(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
