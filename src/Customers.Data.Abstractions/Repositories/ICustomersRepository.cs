using Customers.Model.Entities;

namespace Customers.Data.Abstractions.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetAsync(string email);
        IEnumerable<Customer> GetAll();
        Task InsertAsync(Customer customer);
        void DeleteAsync(Customer customer);
    }
}
