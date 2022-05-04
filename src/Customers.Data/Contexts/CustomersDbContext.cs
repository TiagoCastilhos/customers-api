using Customers.Data.Abstractions;
using Customers.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Data.Contexts
{
    internal sealed class CustomersDbContext : DbContext, ICustomersUnitOfWork
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);
        }

        public async Task<int> SaveAsync()
            => await SaveChangesAsync();
    }
}
