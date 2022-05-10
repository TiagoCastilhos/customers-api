using Customers.Api.Middlewares;
using Customers.Model.Exceptions;
using Customers.Tests.Common;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Customers.Api.Tests.Middlewares
{
    public class AssertionFailedExceptionHandlerMiddlewareTests : TestBase
    {
        [Fact]
        public async void InvokeAsync_RequestDelegateDidntThrowException_StatusCodeShouldNotBeChanged()
        {
            // arrange
            int expectedStatusCode = 418;

            var responseMock = new Mock<HttpResponse>();
            responseMock.Setup(r => r.StatusCode).Returns(expectedStatusCode);

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(r => r.Response).Returns(responseMock.Object);

            var requestDelegate = new RequestDelegate((context) => Task.FromResult(context));

            var middleware = new AssertionFailedExceptionHandlerMiddleware(requestDelegate);

            // act
            await middleware.InvokeAsync(contextMock.Object);

            // assert
            Assert.Equal(expectedStatusCode, responseMock.Object.StatusCode);
        }

        [Fact]
        public async void InvokeAsync_RequestDelegateThrowsAssertionConcernFailedException_StatusCodeShouldBeBadRequest()
        {
            // arrange
            int initialStatusCode = 418;
            int expectedStatusCode = 400;

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            var requestDelegate = new RequestDelegate((context) => Task.FromException<AssertionConcernFailedException>(new AssertionConcernFailedException("test")));

            var middleware = new AssertionFailedExceptionHandlerMiddleware(requestDelegate);

            // act
            await middleware.InvokeAsync(httpContext);

            // assert
            Assert.NotEqual(initialStatusCode, httpContext.Response.StatusCode);
            Assert.Equal(expectedStatusCode, httpContext.Response.StatusCode);
        }

        [Fact]
        public async void InvokeAsync_RequestDelegateThrowsEntityAlreadyExistsException_StatusCodeShouldBeConflict()
        {
            // arrange
            int initialStatusCode = 418;
            int expectedStatusCode = 409;

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            var requestDelegate = new RequestDelegate((context) => Task.FromException<EntityAlreadyExistsException>(new EntityAlreadyExistsException("test", "test")));

            var middleware = new AssertionFailedExceptionHandlerMiddleware(requestDelegate);

            // act
            await middleware.InvokeAsync(httpContext);

            // assert
            Assert.NotEqual(initialStatusCode, httpContext.Response.StatusCode);
            Assert.Equal(expectedStatusCode, httpContext.Response.StatusCode);
        }

        [Fact]
        public async void InvokeAsync_RequestDelegateThrowsEntityNotFoundException_StatusCodeShouldBeNotFound()
        {
            // arrange
            int initialStatusCode = 418;
            int expectedStatusCode = 404;

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            var requestDelegate = new RequestDelegate((context) => Task.FromException<EntityNotFoundException>(new EntityNotFoundException("test", "test")));

            var middleware = new AssertionFailedExceptionHandlerMiddleware(requestDelegate);

            // act
            await middleware.InvokeAsync(httpContext);

            // assert
            Assert.NotEqual(initialStatusCode, httpContext.Response.StatusCode);
            Assert.Equal(expectedStatusCode, httpContext.Response.StatusCode);
        }

        [Fact]
        public async void InvokeAsync_RequestDelegateThrowsUncaughtException_ShouldThrow()
        {
            // arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            var requestDelegate = new RequestDelegate((context) => Task.FromException<Exception>(new Exception("test")));

            var middleware = new AssertionFailedExceptionHandlerMiddleware(requestDelegate);

            // act
            var exception = await Record.ExceptionAsync(() => middleware.InvokeAsync(httpContext));

            // assert
            Assert.NotNull(exception);
        }
    }
}
