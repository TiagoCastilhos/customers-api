using Customers.Application.Abstractions.Services;
using Customers.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersService, CustomersService>();

            return services;
        }
    }
}