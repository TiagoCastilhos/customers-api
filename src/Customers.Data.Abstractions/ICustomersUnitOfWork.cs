namespace Customers.Data.Abstractions
{
    public interface ICustomersUnitOfWork
    {
        Task<int> SaveAsync();
    }
}