using Customers.Application.Extensions;
using Customers.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Api.Bootstrap.Extensions
{
    public static class ServiceColletionExtensions
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddData(configuration);
            services.AddApplication();
        }
    }
}