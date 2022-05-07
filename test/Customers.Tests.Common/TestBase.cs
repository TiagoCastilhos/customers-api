using AutoFixture;
using AutoFixture.AutoMoq;
using Customers.Model.Dtos;
using Customers.Model.Entities;
using System;

namespace Customers.Tests.Common
{
    public abstract class TestBase
    {
        protected IFixture Fixture { get; }

        public TestBase()
        {
            Fixture = new Fixture();

            Fixture.Customize(new AutoMoqCustomization()
            {
                ConfigureMembers = true
            });

            Fixture
                .Customize<Customer>(c => c.FromFactory(() => new Customer($"email{Guid.NewGuid()}@test.com", $"customer{Guid.NewGuid()}")));

            Fixture
                .Customize<CustomerInputModel>(c => c.FromFactory(() => new CustomerInputModel($"email{Guid.NewGuid()}@test.com", $"customer{Guid.NewGuid()}")));
        }
    }
}
