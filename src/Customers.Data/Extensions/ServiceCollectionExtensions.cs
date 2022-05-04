using Customers.Data.Abstractions;
using Customers.Data.Abstractions.Repositories;
using Customers.Data.Contexts;
using Customers.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContexts(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ICustomersUnitOfWork, CustomersDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("CustomersDb"));
                });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomersRepository, CustomersRepository>();

            return services;
        }
    }
}