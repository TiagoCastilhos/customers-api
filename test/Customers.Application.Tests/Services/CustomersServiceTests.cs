using AutoFixture;
using Customers.Application.Services;
using Customers.Data.Abstractions;
using Customers.Data.Abstractions.Repositories;
using Customers.Model.Dtos;
using Customers.Model.Entities;
using Customers.Model.Exceptions;
using Customers.Tests.Common;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Customers.Application.Tests.Services
{
    public class CustomersServiceTests : TestBase
    {
        [Fact]
        public void GetAll_CustomerExists_ShouldReturnCustomer()
        {
            // arrange
            var customersFixture = Fixture.CreateMany<Customer>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            repositoryMock.Setup(r => r.GetAll()).Returns(customersFixture);

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act
            var customers = service.GetAll();

            // assert
            Assert.True(customers.Any());
        }

        [Fact]
        public async Task Get_CustomerExists_ShouldReturnCustomer()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            repositoryMock.Setup(r => r.GetAsync(customerFixture.Email)).ReturnsAsync(customerFixture);

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act
            var customer = await service.GetAsync(customerFixture.Email);

            // assert
            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Get_CustomerDoesNotExist_ShouldThrowEntityNotFoundException()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act & assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetAsync(customerFixture.Email));
        }

        [Fact]
        public async Task Insert_CustomerDoesNotExist_ShouldInsertCustomer()
        {
            // arrange
            var customerFixture = Fixture.Create<CustomerInputModel>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act
            var exception = await Record.ExceptionAsync(() => service.InsertAsync(customerFixture));

            // assert
            Assert.Null(exception);
            unitOfWorkMock.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Insert_CustomerExists_ShouldThrowEntityAlreadyExistsException()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var customerInputModelFixture = Fixture.Create<CustomerInputModel>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            repositoryMock.Setup(r => r.GetAsync(customerInputModelFixture.Email)).ReturnsAsync(customerFixture);

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act
            var exception = await Record.ExceptionAsync(() => service.InsertAsync(customerInputModelFixture));

            // assert
            Assert.IsType<EntityAlreadyExistsException>(exception);
            unitOfWorkMock.Verify(m => m.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Update_CustomerExists_ShouldUpdateCustomer()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var customerInputModelFixture = Fixture.Create<CustomerInputModel>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            repositoryMock.Setup(r => r.GetAsync(customerFixture.Email)).ReturnsAsync(customerFixture);

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act
            await service.UpdateAsync(customerFixture.Email, customerInputModelFixture);

            // assert
            unitOfWorkMock.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_CustomerDoesNotExist_ShouldThrowEntityNotFoundException()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var customerInputModelFixture = Fixture.Create<CustomerInputModel>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act & assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(customerFixture.Email, customerInputModelFixture));
            unitOfWorkMock.Verify(m => m.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_CustomerExists_ShouldDeleteCustomer()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            repositoryMock.Setup(r => r.GetAsync(customerFixture.Email)).ReturnsAsync(customerFixture);

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act
            var exception = await Record.ExceptionAsync(() => service.DeleteAsync(customerFixture.Email));

            // assert
            Assert.Null(exception);
            unitOfWorkMock.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_CustomerDoesNotExist_ShouldThrowEntityNotFoundException()
        {
            // arrange
            var customerFixture = Fixture.Create<Customer>();
            var repositoryMock = new Mock<ICustomersRepository>();
            var unitOfWorkMock = new Mock<ICustomersUnitOfWork>();

            var service = new CustomersService(repositoryMock.Object, unitOfWorkMock.Object);

            // act & assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(customerFixture.Email));
        }
    }
}
