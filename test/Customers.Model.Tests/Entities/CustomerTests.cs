using Customers.Model.Entities;
using Customers.Model.Exceptions;
using Xunit;

namespace Customers.Model.Tests.Entities
{
    public sealed class CustomerTests
    {
        [Fact]
        public void Constructor_ArgumentsAreValid_ShouldCreate()
        {
            // act
            var exception = Record.Exception(() => new Customer("test@test.com", "test"));

            // assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_EmailIsNullOrEmpty_ShouldThrowAssertionArgumentNullException(string email)
        {
            // act
            var exception = Record.Exception(() => new Customer(email, "test"));

            // assert
            Assert.IsType<AssertionArgumentNullException>(exception);
        }

        [Fact]
        public void Constructor_EmailIsBadFormatted_ShouldThrowInvalidEmailException()
        {
            // act
            var exception = Record.Exception(() => new Customer("test", "test"));

            // assert
            Assert.IsType<InvalidEmailException>(exception);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_NameIsNullOrEmpty_ShouldThrowAssertionArgumentNullException(string name)
        {
            // act
            var exception = Record.Exception(() => new Customer("test@test.com", name));

            // assert
            Assert.IsType<AssertionArgumentNullException>(exception);
        }

        [Fact]
        public void UpdateEmail_ArgumentIsValid_ShouldCreate()
        {
            // arrange
            var customer = new Customer("test@test.com", "test");
            var newEmail = "test2@test.com";

            // act
            customer.UpdateEmail(newEmail);

            // assert
            Assert.Equal(newEmail, customer.Email);
        }

        [Fact]
        public void Rename_ArgumentIsValid_ShouldCreate()
        {
            // arrange
            var customer = new Customer("test@test.com", "test");
            var newName = "test2";

            // act
            customer.Rename(newName);

            // assert
            Assert.Equal(newName, customer.Name);
        }
    }
}