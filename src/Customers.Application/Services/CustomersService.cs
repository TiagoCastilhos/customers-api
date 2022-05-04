using Customers.Application.Abstractions.Services;
using Customers.Data.Abstractions;
using Customers.Data.Abstractions.Repositories;
using Customers.Model;
using Customers.Model.Dtos;
using Customers.Model.Entities;

namespace Customers.Application.Services
{
    internal sealed class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ICustomersUnitOfWork _customersUnitOfWork;

        public CustomersService(ICustomersRepository customersRepository, ICustomersUnitOfWork customersUnitOfWork)
        {
            _customersRepository = customersRepository;
            _customersUnitOfWork = customersUnitOfWork;
        }

        public IEnumerable<CustomerOutputModel> GetAll()
            => _customersRepository
                .GetAll()
                .Select(c => ToOutput(c))
                .ToList();

        public async Task<CustomerOutputModel> GetAsync(string email)
            => ToOutput(await GetByEmailAsync(email));

        public async Task<CustomerOutputModel> InsertAsync(CustomerInputModel customerInputModel)
        {
            AssertionConcern.AssertArgumentNotNull(customerInputModel, nameof(customerInputModel));

            var model = ToModel(customerInputModel);

            await _customersRepository.InsertAsync(model);
            await _customersUnitOfWork.SaveAsync();

            return ToOutput(model);
        }

        public async Task UpdateAsync(CustomerInputModel customerInputModel)
        {
            AssertionConcern.AssertArgumentNotNull(customerInputModel, nameof(customerInputModel));

            var customer = await _customersRepository.GetAsync(customerInputModel.Email);

            customer.UpdateEmail(customerInputModel.Email);
            customer.Rename(customerInputModel.Name);

            await _customersUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(string email)
        {
            var customer = await GetByEmailAsync(email);

            _customersRepository.DeleteAsync(customer);
            await _customersUnitOfWork.SaveAsync();
        }

        private async Task<Customer> GetByEmailAsync(string email)
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(email, nameof(email));
            AssertionConcern.AssertArgumentIsEmail(email);

            return await _customersRepository.GetAsync(email);
        }

        private CustomerOutputModel ToOutput(Customer model)
            => new() { Email = model.Email, Name = model.Name, Id = model.Id };

        private Customer ToModel(CustomerInputModel input)
            => new(input.Email, input.Name);
    }
}