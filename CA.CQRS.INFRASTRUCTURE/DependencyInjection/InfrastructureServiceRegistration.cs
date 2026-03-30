using Microsoft.EntityFrameworkCore;
using CA.CQRS.APPLICATION.Interfaces;
using CA.CQRS.INFRASTRUCTURE.DbContext;
using CA.CQRS.INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.INFRASTRUCTURE.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
