using Customers.Model.Types;

namespace Customers.Model.Entities
{
    public class Customer
    {
        public Guid Id { get; protected set; }
        public Email Email { get; protected set; }
        public string Name { get; protected set; }

        public Customer(string email, string name)
        {
            Id = Guid.NewGuid();
            UpdateEmail(email);
            Rename(name);
        }

        protected Customer() { }

        public void UpdateEmail(string email)
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(email, nameof(email));
            AssertionConcern.AssertArgumentIsEmail(email);

            Email = email;
        }

        public void Rename(string name)
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(name, nameof(name));

            Name = name;
        }
    }
}
