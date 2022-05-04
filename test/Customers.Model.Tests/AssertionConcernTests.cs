using Customers.Model.Entities;
using Customers.Model.Exceptions;
using Xunit;

namespace Customers.Model.Tests
{
    public class AssertionConcernTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AssertArgumentNotNullOrEmpty_ArgumentIsNullOrEmpty_ShouldThrowAssertionArgumentNullException(string argument)
        {
            // act
            var exception = Record.Exception(() => AssertionConcern.AssertArgumentNotNullOrEmpty(argument, nameof(argument)));

            // arrange
            Assert.IsType<AssertionArgumentNullException>(exception);
        }

        [Fact]
        public void AssertArgumentNotNull_ArgumentIsNull_ShouldThrowAssertionArgumentNullException()
        {
            // act
            var exception = Record.Exception(() => AssertionConcern.AssertArgumentNotNull<Customer>(null, "argument"));

            // arrange
            Assert.IsType<AssertionArgumentNullException>(exception);
        }

        [Theory]
        [InlineData("test@@test.com")]
        [InlineData("test")]
        public void AssertArgumentIsEmail_ArgumentIsNotAnEmail_ShouldThrowInvalidEmailException(string argument)
        {
            // act
            var exception = Record.Exception(() => AssertionConcern.AssertArgumentIsEmail(argument));

            //arrange
            Assert.IsType<InvalidEmailException>(exception);
        }
    }
}