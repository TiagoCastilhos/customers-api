using Customers.Model.Dtos;

namespace Customers.Application.Abstractions.Services
{
    public interface ICustomersService
    {
        IEnumerable<CustomerOutputModel> GetAll();
        Task<CustomerOutputModel> GetAsync(string email);
        Task<CustomerOutputModel> InsertAsync(CustomerInputModel customerInputModel);
        Task UpdateAsync(string email, CustomerInputModel customerInputModel);
        Task DeleteAsync(string email);
    }
}