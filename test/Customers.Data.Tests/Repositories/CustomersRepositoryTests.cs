using AutoFixture;
using Customers.Data.Contexts;
using Customers.Data.Repositories;
using Customers.Model.Entities;
using Customers.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customers.Data.Tests.Repositories
{
    public class CustomersRepositoryTests : TestBase
    {
        private readonly CustomersDbContext _customersDbContext;

        public CustomersRepositoryTests()
            : base()
        {
            var options = new DbContextOptionsBuilder();
            options.UseInMemoryDatabase("customers-sqldb");

            _customersDbContext = new CustomersDbContext(options.Options);
            _customersDbContext.Customers.AddRange(Fixture.CreateMany<Customer>());
            _customersDbContext.SaveChanges();
        }

        [Fact]
        public void GetAll_DatabaseHasCustomers_ShouldHaveAnyCustomer()
        {
            // arrange
            var repository = new CustomersRepository(_customersDbContext);

            // act
            var customers = repository.GetAll();

            // assert
            Assert.True(customers.Any());
        }

        [Fact]
        public async Task Get_CustomerWithProvidedEmailExists_ShouldRetrieveCustomer()
        {
            // arrange
            var repository = new CustomersRepository(_customersDbContext);

            // act
            var customer = await repository.GetAsync((await _customersDbContext.Customers.FirstAsync()).Email);

            // assert
            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Insert_CustomerIsProvided_ShouldInsertCustomer()
        {
            // arrange
            var repository = new CustomersRepository(_customersDbContext);

            // act
            await repository.InsertAsync(Fixture.Create<Customer>());

            // assert
            Assert.True(_customersDbContext.SaveChanges() > 0);
        }

        [Fact]
        public async Task Delete_CustomerExistsInDatabase_ShouldDeleteCustomer()
        {
            // arrange
            var repository = new CustomersRepository(_customersDbContext);

            // act
            repository.Delete(await _customersDbContext.Customers.FirstAsync());

            // assert
            Assert.True(_customersDbContext.SaveChanges() > 0);
        }
    }
}
