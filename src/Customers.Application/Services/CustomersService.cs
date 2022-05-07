using Customers.Application.Abstractions.Services;
using Customers.Data.Abstractions;
using Customers.Data.Abstractions.Repositories;
using Customers.Model;
using Customers.Model.Dtos;
using Customers.Model.Entities;
using Customers.Model.Exceptions;

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
        {
            var customer = await GetByEmailAsync(email);

            if (customer == null)
                throw new EntityNotFoundException(email, "Customer");

            return ToOutput(customer);
        }

        public async Task<CustomerOutputModel> InsertAsync(CustomerInputModel customerInputModel)
        {
            AssertionConcern.AssertArgumentNotNull(customerInputModel, nameof(customerInputModel));

            var existingCustomer = await _customersRepository.GetAsync(customerInputModel.Email);

            if (existingCustomer != null)
                throw new EntityAlreadyExistsException(customerInputModel.Email, "Customer");

            var model = ToModel(customerInputModel);

            await _customersRepository.InsertAsync(model);
            await _customersUnitOfWork.SaveAsync();

            return ToOutput(model);
        }

        public async Task UpdateAsync(string email, CustomerInputModel customerInputModel)
        {
            AssertionConcern.AssertArgumentNotNull(customerInputModel, nameof(customerInputModel));

            var customer = await _customersRepository.GetAsync(email);

            if (customer == null)
                throw new EntityNotFoundException(email, "Customer");

            customer.UpdateEmail(customerInputModel.Email);
            customer.Rename(customerInputModel.Name);

            await _customersUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(string email)
        {
            ValidateEmailArgument(email);
            var customer = await GetByEmailAsync(email);

            if (customer == null)
                throw new EntityNotFoundException(email, "Customer");

            _customersRepository.Delete(customer);
            await _customersUnitOfWork.SaveAsync();
        }

        private async Task<Customer> GetByEmailAsync(string email)
        {
            ValidateEmailArgument(email);

            return await _customersRepository.GetAsync(email);
        }

        private static void ValidateEmailArgument(string email)
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(email, nameof(email));
            AssertionConcern.AssertArgumentIsEmail(email);
        }

        private CustomerOutputModel ToOutput(Customer model)
            => new() { Email = model.Email, Name = model.Name, Id = model.Id };

        private Customer ToModel(CustomerInputModel input)
            => new(input.Email, input.Name);
    }
}