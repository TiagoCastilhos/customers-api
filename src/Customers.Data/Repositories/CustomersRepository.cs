using Customers.Data.Abstractions.Repositories;
using Customers.Data.Contexts;
using Customers.Model;
using Customers.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Data.Repositories
{
    internal sealed class CustomersRepository : ICustomersRepository
    {
        private readonly CustomersDbContext _customersDbContext;

        public CustomersRepository(CustomersDbContext customersDbContext)
        {
            _customersDbContext = customersDbContext;
        }

        public async Task<Customer> GetAsync(string email)
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(email, nameof(email));

            return await _customersDbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customersDbContext.Customers.AsEnumerable();
        }

        public async Task InsertAsync(Customer customer)
        {
            AssertionConcern.AssertArgumentNotNull(customer, nameof(customer));
            
            await _customersDbContext.Customers.AddAsync(customer);
        }

        public void Delete(Customer customer)
        {
            AssertionConcern.AssertArgumentNotNull(customer, nameof(customer));

            _customersDbContext.Customers.Remove(customer);
        }
    }
}